using System.Runtime.Serialization;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace ErrorHandling.Exceptions;

/// <summary>
///     Exception thrown when an unknown or unexpected error occurs in the application.
/// </summary>
[Serializable]
public class UnKnownException : ApplicationFailureException, ICodedException<BackendErrorType>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnKnownException" /> class.
    /// </summary>
    public UnKnownException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnKnownException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public UnKnownException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnKnownException" /> class with a specified error message
    ///     and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public UnKnownException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UnKnownException" /> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information.</param>
    public UnKnownException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    ///     Gets the error code associated with this exception.
    /// </summary>
    /// <returns>The <see cref="BackendErrorType.UnKnownException" /> error code.</returns>
    public new BackendErrorType GetErrorCode()
    {
        return BackendErrorType.UnKnownException;
    }
}