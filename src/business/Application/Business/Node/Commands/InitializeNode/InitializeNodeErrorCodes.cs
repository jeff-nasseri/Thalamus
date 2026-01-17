using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Node.Commands.InitializeNode;

/// <summary>
///     Error codes for node initialization operations.
/// </summary>
[HandlerCode(HandlerCode.InitializeNode)]
public enum InitializeNodeErrorCodes
{
    /// <summary>
    ///     Indicates that node entities could not be mapped from DTOs.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    NodeMappingFailed = 1,

    /// <summary>
    ///     Indicates that inserting nodes into the repository failed due to database exception.
    /// </summary>
    [ErrorType(BackendErrorType.DbException)]
    NodeInsertFailed = 2,

    /// <summary>
    ///     Indicates an unknown error occurred during node initialization.
    /// </summary>
    [ErrorType(BackendErrorType.UnKnownException)]
    UnknownInitializationError = 99
}