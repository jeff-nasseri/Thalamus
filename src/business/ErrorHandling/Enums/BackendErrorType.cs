using ErrorHandling.Attributes;

namespace ErrorHandling.Enums;

/// <summary>
/// Defines the backend error type classifications used for error categorization and handling.
/// Each type is associated with a <see cref="ServerErrorGroup"/> to determine responsibility and handling strategy.
/// </summary>
public enum BackendErrorType
{
    /// <summary>
    /// Indicates a business logic error (e.g., invalid operation according to business rules).
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.BusinessLogic)]
    BusinessLogic,

    /// <summary>
    /// Indicates a database exception occurred during data access operations.
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.ApplicationFailure)]
    DbException,

    /// <summary>
    /// Indicates a failed query or command to the database due to invalid query structure.
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.ApplicationFailure)]
    InvalidDatabaseQuery,

    /// <summary>
    /// Indicates the application failed to communicate with a third-party service.
    /// For example, SMS service provider failure when sending 2FA codes.
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.SystemFailure)]
    ThirdPartyFailure,

    /// <summary>
    /// Indicates a platform-level failure (e.g., infrastructure or environment issues).
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.SystemFailure)]
    PlatformFailure,

    /// <summary>
    /// Indicates a security attempt where a user tries to bypass validation or security measures.
    /// The system may respond with a fake success message while alerting administrators.
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.Security)]
    SecurityAttempt,

    /// <summary>
    /// Indicates a security breach where invalid data was inserted into the database
    /// after passing all security and validation layers.
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.Security)]
    SecurityBreached,

    /// <summary>
    /// Indicates an unknown or unexpected exception occurred in the application.
    /// </summary>
    [ServerErrorGroup(ServerErrorGroup.ApplicationFailure)]
    UnKnownException
}