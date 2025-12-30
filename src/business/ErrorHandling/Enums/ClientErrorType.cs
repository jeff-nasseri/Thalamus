namespace ErrorHandling.Enums;

/// <summary>
/// Defines client-facing error type classifications.
/// These types are exposed to clients and map from backend error types.
/// </summary>
public enum ClientErrorType
{
    /// <summary>
    /// Indicates a business logic error that the client can handle or display to the user.
    /// </summary>
    BusinessLogic = 1,

    /// <summary>
    /// Indicates an internal server error that should not expose implementation details to the client.
    /// </summary>
    InternalServerError = 2
}