using ErrorHandling.Attributes;

namespace ErrorHandling.Enums;

/// <summary>
/// Defines Point of Failure (POF) scenarios that can make endpoints inaccessible.
/// These represent critical failure points requiring immediate attention.
/// </summary>
public enum PointOfFailure
{
    /// <summary>
    /// Indicates the database is not available and requires immediate attention.
    /// </summary>
    [ErrorType(BackendErrorType.PlatformFailure)]
    DatabaseIsNotAvailable = 99,
}