using Thalamus.Web.Swagger.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Thalamus.Web.Swagger;

/// <summary>
/// Configures Swagger generation options for the application.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="apiDescriptionGroupCollectionProvider">Provider for API description groups.</param>
    public ConfigureSwaggerOptions(
        IConfiguration configuration,
        IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
    {
        _configuration = configuration;
        _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
    }

    /// <summary>
    /// Configures Swagger generation options including versioning, filters, and schema mappings.
    /// </summary>
    /// <param name="options">Swagger generation options to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiDescriptionGroup? apiDescriptionGroup in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups
                     .Items)
        {
            options.SwaggerDoc(apiDescriptionGroup.GroupName, CreateVersionInfo(apiDescriptionGroup.Items.First()));
        }

        options.EnableAnnotations();

        options.AddEnumsWithValuesFixFilters();
        options.UseAllOfToExtendReferenceSchemas();

        options.OperationFilter<ErrorCodeOperationFilter>();
        options.OperationFilter<RemoveApiVersionHeaderFilter>();

        options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
    }

    /// <summary>
    /// Creates OpenAPI version information for a specific API description.
    /// </summary>
    /// <param name="description">The API description to create version info for.</param>
    /// <returns>OpenAPI version information including title and deprecation status.</returns>
    private OpenApiInfo CreateVersionInfo(ApiDescription description)
    {
        string appName = _configuration.GetValue<string>("AppName")! ?? "Payment";

        OpenApiInfo info = new()
        {
            Title = $"{appName}-{description.GroupName}",
            Version = description.GetApiVersion().ToString()
        };

        if (description.IsDeprecated())
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}