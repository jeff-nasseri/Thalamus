using System.Runtime.Serialization;
using ErrorHandling.Attributes;

namespace ErrorHandling.Exceptions;

/// <summary>
///     Exception thrown when an error codes enum is missing the required <see cref="HandlerCodeAttribute" />.
/// </summary>
[Serializable]
public class HandlerErrorsAttributeIsMissingException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HandlerErrorsAttributeIsMissingException" /> class.
    /// </summary>
    /// <param name="enumName">The name of the enum that is missing the attribute.</param>
    public HandlerErrorsAttributeIsMissingException(string enumName)
        : base($"'{enumName}' must be annotated with '{nameof(HandlerCodeAttribute)}'.")
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="HandlerErrorsAttributeIsMissingException" /> class with serialized
    ///     data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information.</param>
    protected HandlerErrorsAttributeIsMissingException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}