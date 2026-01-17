using System.Reflection;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Thalamus.Web.Swagger;

/// <summary>
///     Provides extension methods for API description analysis.
/// </summary>
public static class ApiDescriptionExtensions
{
    /// <summary>
    ///     Extracts the handler code from an API description by examining endpoint metadata and request parameters.
    /// </summary>
    /// <param name="apiDescription">The API description to analyze.</param>
    /// <param name="requestType">Output parameter containing the identified request type, if found.</param>
    /// <returns>The handler code if found; otherwise, null.</returns>
    public static HandlerCode? GetHandlerCode(this ApiDescription apiDescription, out Type? requestType)
    {
        requestType = null;

        if (apiDescription.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(a => a is SwaggerRequestTypeAttribute) is SwaggerRequestTypeAttribute
            swaggerRequestTypeAttribute)
            requestType = swaggerRequestTypeAttribute.Type;

        requestType ??= apiDescription.ActionDescriptor.Parameters
            .FirstOrDefault(p => p.ParameterType.IsAssignableTo(typeof(IBaseRequest)))?.ParameterType;

        if (requestType is not null) return requestType.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode;

        return null;
    }
}