using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Application;
using ErrorHandling;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Thalamus.Web.Swagger.Filters;

/// <summary>
/// Swagger operation filter that enriches API documentation with error codes and custom response schemas.
/// </summary>
public class ErrorCodeOperationFilter : IOperationFilter
{
    private readonly Dictionary<string, OpenApiSchema> _deletedSchemas = new();
    private readonly Dictionary<string, string> _errorMessages;
    private readonly Dictionary<HandlerCode, Type> _handlerCodes;
    private readonly bool _useAllOfToExtendReferenceSchemas;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorCodeOperationFilter"/> class.
    /// </summary>
    /// <param name="options">Schema generator options.</param>
    public ErrorCodeOperationFilter(SchemaGeneratorOptions options)
    {
        _useAllOfToExtendReferenceSchemas = options.UseAllOfToExtendReferenceSchemas;

        _handlerCodes = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(a => a.IsEnum && a.GetCustomAttribute<HandlerCodeAttribute>() is not null)
            .ToDictionary(a => a.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode);

        _errorMessages = ResourceHelper.GetAllErrorMessages(typeof(DependencyInjection).Assembly);
    }

    /// <summary>
    /// Applies the filter to enrich the operation with error code documentation.
    /// </summary>
    /// <param name="operation">The OpenAPI operation to modify.</param>
    /// <param name="context">The operation filter context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        string? responseName = operation.Responses.First().Value.Content.Values.First().Schema.Reference.Id;

        HandlerCode? handlerCode = context.ApiDescription.GetHandlerCode(out Type? requestType);

        if (handlerCode is not null && requestType is not null &&
            _handlerCodes.TryGetValue(handlerCode.Value, out Type? errorCodesEnumType))
        {
            string requestName = requestType.Name;
            string formattedHandlerCode = ((int)handlerCode).ToString().Insert(2, "_");
            operation.Summary = operation.Summary is null
                ? formattedHandlerCode
                : formattedHandlerCode + " — " + operation.Summary;
            operation.Description = GenerateApiDescription(errorCodesEnumType);

            OverrideResponseSchema(operation, context, errorCodesEnumType, requestName, responseName);
        }
    }

    /// <summary>
    /// Overrides the response schema with custom error code schemas.
    /// </summary>
    /// <param name="operation">The OpenAPI operation to modify.</param>
    /// <param name="context">The operation filter context.</param>
    /// <param name="errorCodesEnumType">The error codes enum type.</param>
    /// <param name="requestName">The request name.</param>
    /// <param name="responseName">The response name.</param>
    private void OverrideResponseSchema(
        OpenApiOperation operation,
        OperationFilterContext context,
        Type errorCodesEnumType,
        string requestName,
        string responseName)
    {
        OpenApiSchema rawErrorSchema = GetRawErrorSchema(context);

        OpenApiSchema errorSchema = GenerateErrorSchema(context, rawErrorSchema, errorCodesEnumType, requestName);
        errorSchema = _useAllOfToExtendReferenceSchemas ? WrapWithAllOfSchemaType(errorSchema, true) : errorSchema;

        OpenApiSchema? responseSchema =
            context.SchemaRepository.Schemas.TryGetValue(responseName, out OpenApiSchema? foundSchema)
                ? foundSchema
                : _deletedSchemas[responseName];

        if (responseName == nameof(Response))
        {
            OpenApiSchema newResponseSchema =
                ReplaceNonGenericResponseSchema(context, responseSchema, errorSchema, requestName);

            SetOperationResponseSchema(operation, newResponseSchema);
        }
        else
        {
            OpenApiSchema newResponseSchema =
                RenameResponseSchema(context, responseSchema, errorSchema, requestName, responseName);

            SetOperationResponseSchema(operation, newResponseSchema);
        }
    }

    /// <summary>
    /// Sets the response schema for all responses in the operation.
    /// </summary>
    /// <param name="operation">The OpenAPI operation to modify.</param>
    /// <param name="specificResponseSchema">The schema to apply.</param>
    private void SetOperationResponseSchema(OpenApiOperation operation, OpenApiSchema specificResponseSchema)
    {
        foreach (KeyValuePair<string, OpenApiResponse> openApiResponse in operation.Responses)
        foreach (KeyValuePair<string, OpenApiMediaType> openApiMediaType in openApiResponse.Value.Content)
        {
            openApiMediaType.Value.Schema = specificResponseSchema;
        }
    }

    /// <summary>
    /// Renames and modifies a response schema with the error schema.
    /// </summary>
    /// <param name="context">The operation filter context.</param>
    /// <param name="responseSchema">The original response schema.</param>
    /// <param name="errorSchema">The error schema to add.</param>
    /// <param name="requestName">The request name.</param>
    /// <param name="responseName">The original response name.</param>
    /// <returns>The modified response schema.</returns>
    private OpenApiSchema RenameResponseSchema(
        OperationFilterContext context,
        OpenApiSchema responseSchema,
        OpenApiSchema errorSchema,
        string requestName,
        string responseName)
    {
        context.SchemaRepository.Schemas.Remove(responseName);
        _deletedSchemas.TryAdd(responseName, responseSchema);
        OpenApiSchema newResponseSchema = new(responseSchema);
        newResponseSchema.Properties["error"] = errorSchema;
        string schemaKey = Regex.Replace(requestName, "Request$", "Response");

        if (context.SchemaRepository.Schemas.TryGetValue(schemaKey, out OpenApiSchema _))
        {
            return new OpenApiSchema
            {
                Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = schemaKey }
            };
        }

        OpenApiSchema? generatedResponseSchemaReference =
            context.SchemaRepository.AddDefinition(schemaKey, newResponseSchema);
        return generatedResponseSchemaReference;
    }

    /// <summary>
    /// Replaces a non-generic response schema with a version that includes the error schema.
    /// </summary>
    /// <param name="context">The operation filter context.</param>
    /// <param name="responseSchema">The original response schema.</param>
    /// <param name="errorSchema">The error schema to add.</param>
    /// <param name="requestName">The request name.</param>
    /// <returns>The modified response schema.</returns>
    private OpenApiSchema ReplaceNonGenericResponseSchema(
        OperationFilterContext context,
        OpenApiSchema responseSchema,
        OpenApiSchema errorSchema,
        string requestName)
    {
        OpenApiSchema complexResponseSchema = new(responseSchema);
        complexResponseSchema.Properties["error"] = errorSchema;
        string schemaKey = Regex.Replace(requestName, "Request$", "Response");

        if (context.SchemaRepository.Schemas.TryGetValue(schemaKey, out OpenApiSchema _))
        {
            return new OpenApiSchema
            {
                Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = schemaKey }
            };
        }

        OpenApiSchema? generatedResponseSchemaReference =
            context.SchemaRepository.AddDefinition(schemaKey, complexResponseSchema);
        return generatedResponseSchemaReference;
    }

    /// <summary>
    /// Generates an error schema specific to the request type.
    /// </summary>
    /// <param name="context">The operation filter context.</param>
    /// <param name="rawErrorSchema">The base error schema.</param>
    /// <param name="errorCodesEnumType">The error codes enum type.</param>
    /// <param name="requestName">The request name.</param>
    /// <returns>The generated error schema.</returns>
    private OpenApiSchema GenerateErrorSchema(
        OperationFilterContext context,
        OpenApiSchema rawErrorSchema,
        Type errorCodesEnumType,
        string requestName)
    {
        OpenApiSchema errorSchema = new(rawErrorSchema);
        OpenApiSchema? errorCodesEnumSchema =
            context.SchemaGenerator.GenerateSchema(errorCodesEnumType, context.SchemaRepository);
        errorSchema.Properties["code"] =
            _useAllOfToExtendReferenceSchemas ? WrapWithAllOfSchemaType(errorCodesEnumSchema) : errorCodesEnumSchema;
        string schemaKey = Regex.Replace(requestName, "Request$", "Error");

        if (context.SchemaRepository.Schemas.TryGetValue(schemaKey, out OpenApiSchema _))
        {
            return new OpenApiSchema
            {
                Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = schemaKey }
            };
        }

        OpenApiSchema? errorSchemaReference = context.SchemaRepository.AddDefinition(schemaKey, errorSchema);
        return errorSchemaReference;
    }

    /// <summary>
    /// Retrieves or generates the raw error schema.
    /// </summary>
    /// <param name="context">The operation filter context.</param>
    /// <returns>The raw error schema.</returns>
    private OpenApiSchema GetRawErrorSchema(OperationFilterContext context)
    {
        if (!context.SchemaRepository.Schemas.TryGetValue("Error", out OpenApiSchema? errorSchema))
        {
            context.SchemaGenerator.GenerateSchema(typeof(Error), context.SchemaRepository);
            errorSchema = context.SchemaRepository.Schemas["Error"];
        }

        return errorSchema;
    }

    /// <summary>
    /// Wraps a schema with an allOf construct for schema extension.
    /// </summary>
    /// <param name="schema">The schema to wrap.</param>
    /// <param name="nullable">Whether the schema is nullable.</param>
    /// <returns>The wrapped schema.</returns>
    private OpenApiSchema WrapWithAllOfSchemaType(OpenApiSchema schema, bool nullable = false)
    {
        OpenApiSchema wrappedSchema = new()
        {
            AllOf = new List<OpenApiSchema>
            {
                schema
            },
            Nullable = nullable,
            ReadOnly = true
        };
        return wrappedSchema;
    }

    /// <summary>
    /// Generates API description with error codes table in markdown format.
    /// </summary>
    /// <param name="errorType">The error enum type.</param>
    /// <returns>Markdown formatted description with error codes table.</returns>
    private string GenerateApiDescription(Type errorType)
    {
        var errorCodes = Enum.GetValues(errorType).Cast<Enum>()
            .Select(a => new
            {
                Code = Convert.ToInt32(a).ToString("n0").Replace(",", "_"),
                Name = a.ToString()
            });

        IEnumerable<string> tableRows = errorCodes.Select(errorCode =>
        {
            _errorMessages.TryGetValue(errorCode.Code, out string? message);
            return $"| {errorCode.Code} | {errorCode.Name} | {message ?? string.Empty} |";
        });

        StringBuilder descriptionTable = new();
        descriptionTable.AppendLine("### Error Codes");
        descriptionTable.AppendLine("| Code | Name | Message |");
        descriptionTable.AppendLine("| :--- | :--- | :--- |");
        descriptionTable.Append(string.Join(Environment.NewLine, tableRows));

        return descriptionTable.ToString();
    }
}