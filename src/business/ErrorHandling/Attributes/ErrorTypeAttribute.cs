using ErrorHandling.Enums;

namespace ErrorHandling.Attributes;

/// <summary>
/// Attribute to specify the backend error type for an error code enum field.
/// Used to categorize errors and determine appropriate error handling strategies.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class ErrorTypeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorTypeAttribute"/> class.
    /// </summary>
    /// <param name="backendErrorType">The backend error type classification.</param>
    public ErrorTypeAttribute(BackendErrorType backendErrorType)
    {
        BackendErrorType = backendErrorType;
    }

    /// <summary>
    /// Gets the backend error type.
    /// </summary>
    public BackendErrorType BackendErrorType { get; }
}