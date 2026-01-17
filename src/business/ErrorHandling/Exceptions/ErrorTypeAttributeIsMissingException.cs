using System.Runtime.Serialization;
using ErrorHandling.Attributes;

namespace ErrorHandling.Exceptions;

/// <summary>
///     Exception thrown when an error code enum field is missing the required <see cref="ErrorTypeAttribute" />.
/// </summary>
[Serializable]
public class ErrorTypeAttributeIsMissingException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ErrorTypeAttributeIsMissingException" /> class.
    /// </summary>
    /// <param name="enumValue">The enum value that is missing the attribute.</param>
    public ErrorTypeAttributeIsMissingException(Enum enumValue)
        : base($"'{enumValue.ToString()}' value of '{enumValue.GetType()}' enum must be annotated " +
               $"with '{nameof(ErrorTypeAttribute)}'.")
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ErrorTypeAttributeIsMissingException" /> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information.</param>
    protected ErrorTypeAttributeIsMissingException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}