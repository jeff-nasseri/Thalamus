using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Mcp.Commands.InitializeMcp;

/// <summary>
///     Error codes for MCP plugin initialization operations.
/// </summary>
[HandlerCode(HandlerCode.InitializeMcp)]
public enum InitializeMcpErrorCodes
{
    /// <summary>
    ///     Indicates that MCP plugin entities could not be mapped from DTOs.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    McpMappingFailed = 1,

    /// <summary>
    ///     Indicates that inserting MCP plugins into the repository failed due to database exception.
    /// </summary>
    [ErrorType(BackendErrorType.DbException)]
    McpInsertFailed = 2,

    /// <summary>
    ///     Indicates an unknown error occurred during MCP plugin initialization.
    /// </summary>
    [ErrorType(BackendErrorType.UnKnownException)]
    UnknownInitializationError = 99
}