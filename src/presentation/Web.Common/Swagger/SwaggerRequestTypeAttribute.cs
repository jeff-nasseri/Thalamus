namespace Thalamus.Web.Swagger;

/// <summary>
/// Attribute to specify the request type for Swagger documentation on controller methods.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class SwaggerRequestTypeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SwaggerRequestTypeAttribute"/> class.
    /// </summary>
    /// <param name="type">The type of the request for this endpoint.</param>
    public SwaggerRequestTypeAttribute(Type type)
    {
        Type = type;
    }

    /// <summary>
    /// Gets the request type for this endpoint.
    /// </summary>
    public Type Type { get; }
}