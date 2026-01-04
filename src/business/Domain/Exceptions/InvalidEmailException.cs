using System.Runtime.Serialization;
using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Domain.Exceptions;

/// <summary>
///     Exception thrown when an email address is invalid.
/// </summary>
[Serializable]
public class InvalidEmailException : AppException, ICodedException<CommonErrorCode>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidEmailException" /> class.
    /// </summary>
    /// <param name="email">The invalid email address.</param>
    public InvalidEmailException(string? email)
        : base($"{email} is not a valid email address.")
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidEmailException" /> class with an inner exception.
    /// </summary>
    /// <param name="email">The invalid email address.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public InvalidEmailException(string? email, Exception innerException)
        : base($"{email} is not a valid email address.", innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidEmailException" /> class with serialized data.
    /// </summary>
    /// <param name="info">The serialization info.</param>
    /// <param name="context">The streaming context.</param>
    protected InvalidEmailException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    ///     Gets the error code associated with this exception.
    /// </summary>
    /// <returns>The common error code for invalid email addresses.</returns>
    public CommonErrorCode GetErrorCode()
    {
        return CommonErrorCode.InvalidEmailAddress;
    }
}