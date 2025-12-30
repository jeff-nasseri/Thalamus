using ErrorHandling.Attributes;

namespace ErrorHandling.Enums;

/// <summary>
/// Defines 3-digit error codes for common errors that are not specific to any request handler.
/// These codes are used in pipeline behaviors and can be appended to handler codes to form composite error codes.
/// </summary>
public enum CommonErrorCode
{
    /// <summary>
    /// Indicates an unexpected error occurred.
    /// </summary>
    [ErrorType(BackendErrorType.UnKnownException)]
    UnexpectedError = 503,

    /// <summary>
    /// Indicates a general database error.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    DatabaseError = 700,

    /// <summary>
    /// Indicates the database connection failed.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    DatabaseConnectionFailed = 701,

    /// <summary>
    /// Indicates saving changes to the database failed.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    SaveChangesFailed = 702,

    /// <summary>
    /// Indicates a database update concurrency conflict occurred.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    DbUpdateConcurrencyFailed = 703,

    /// <summary>
    /// Indicates validation of input data failed.
    /// </summary>
    [ErrorType(BackendErrorType.SecurityAttempt)]
    ValidationFailed = 800,

    /// <summary>
    /// Indicates a general entity error.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    EntityError = 1000,

    /// <summary>
    /// Indicates an authentication error occurred.
    /// </summary>
    [ErrorType(BackendErrorType.SecurityAttempt)]
    AuthenticationError = 1100,

    /// <summary>
    /// Indicates an invalid email address format.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    InvalidEmailAddress = 900,

    /// <summary>
    /// Indicates an invalid hostname was provided.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    InvalidHostName = 609,

    /// <summary>
    /// Indicates an invalid JWT user profile was detected.
    /// </summary>
    [ErrorType(BackendErrorType.SecurityBreached)]
    InvalidJwtUserProfile = 1200
}