using System.Runtime.Serialization;
using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace ErrorHandling.Exceptions;

/// <summary>
/// Exception thrown when an application failure occurs (e.g., internal service communication failure).
/// </summary>
[Serializable]
public class ApplicationFailureException : AppException, ICodedException<ServerErrorGroup>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationFailureException"/> class.
    /// </summary>
    protected ApplicationFailureException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationFailureException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected ApplicationFailureException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationFailureException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    protected ApplicationFailureException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationFailureException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information.</param>
    protected ApplicationFailureException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    /// <returns>The <see cref="ServerErrorGroup.ApplicationFailure"/> error code.</returns>
    public ServerErrorGroup GetErrorCode()
    {
        return ServerErrorGroup.ApplicationFailure;
    }
}