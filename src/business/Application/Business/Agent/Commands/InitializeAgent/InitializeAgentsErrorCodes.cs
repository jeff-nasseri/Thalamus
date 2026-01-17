using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Agent.Commands.InitializeAgent;

/// <summary>
///     Error codes for agent initialization operations.
/// </summary>
[HandlerCode(HandlerCode.InitializeAgents)]
public enum InitializeAgentsErrorCodes
{
    /// <summary>
    ///     Indicates that agent entities could not be mapped from DTOs.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    AgentMappingFailed = 1,

    /// <summary>
    ///     Indicates that inserting agents into the repository failed due to database exception.
    /// </summary>
    [ErrorType(BackendErrorType.DbException)]
    AgentInsertFailed = 2,

    /// <summary>
    ///     Indicates an unknown error occurred during agent initialization.
    /// </summary>
    [ErrorType(BackendErrorType.UnKnownException)]
    UnknownInitializationError = 99
}