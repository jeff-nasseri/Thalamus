using System.Runtime.Serialization;

namespace ErrorHandling.Abstracts;

/// <summary>
/// Base abstract class for all application-specific exceptions.
/// Provides common constructors for exception handling throughout the application.
/// </summary>
[Serializable]
public abstract class AppException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class.
    /// </summary>
    protected AppException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected AppException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    protected AppException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information.</param>
    [Obsolete("Check parent ctor")]
    protected AppException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}