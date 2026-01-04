namespace Domain.Enums;

/// <summary>
///     Represents the lifecycle status of a plan in the agent execution system.
/// </summary>
public enum PlanType
{
    /// <summary>
    ///     The plan has been created and is awaiting approval.
    /// </summary>
    CREATED,

    /// <summary>
    ///     The plan has been approved and is ready for execution.
    /// </summary>
    APPROVED,

    /// <summary>
    ///     The plan has been rejected by the supervisor.
    /// </summary>
    REJECTED,

    /// <summary>
    ///     The plan has just started execution. The master agent sets this status when it begins sending tasks to other
    ///     agents.
    /// </summary>
    JUST_STARTED,

    /// <summary>
    ///     The plan is currently being executed and tasks are in progress.
    /// </summary>
    IN_PROGRESS,

    /// <summary>
    ///     The plan execution has exited due to an error or other reason. Details of the exit should be provided.
    /// </summary>
    EXISTED,

    /// <summary>
    ///     The plan has completed successfully. All required tasks have been implemented by the master agent or other agents.
    /// </summary>
    COMPLETED
}