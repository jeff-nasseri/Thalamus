namespace Domain.Entities;

/// <summary>
///     Defines the operational states an agent can be in.
/// </summary>
public enum AgentStatus
{
    /// <summary>
    ///     Agent is disabled and cannot be started.
    /// </summary>
    DISABLED,

    /// <summary>
    ///     Agent is currently running and processing tasks.
    /// </summary>
    RUNNING,

    /// <summary>
    ///     Agent has been stopped gracefully.
    /// </summary>
    STOPPED,

    /// <summary>
    ///     Agent has crashed due to an unexpected error.
    /// </summary>
    CRASHED
}