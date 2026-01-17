using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Thalamus.Web.Swagger.Filters;

/// <summary>
///     Operation filter that removes or modifies the API version header parameter in Swagger documentation.
/// </summary>
public class RemoveApiVersionHeaderFilter : IOperationFilter
{
    /// <summary>
    ///     Applies the filter to remove the version header for v1.0 or set an example for other versions.
    /// </summary>
    /// <param name="operation">The OpenAPI operation to modify.</param>
    /// <param name="context">The operation filter context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "x-api-version");

        if (versionParameter != null &&
            context.ApiDescription.Properties.TryGetValue(typeof(ApiVersion), out var apiVersion))
        {
            var version = apiVersion.ToString()!;

            if (version == "1.0")
                operation.Parameters.Remove(versionParameter);
            else
                versionParameter.Example = new OpenApiString(version);
        }
    }
}