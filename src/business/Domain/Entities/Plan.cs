using Domain.Common.BaseTypes;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents an execution plan associated with a specific prompt.
/// Plans are created in response to prompts in agent-to-agent or human-to-agent conversations.
/// </summary>
public class Plan : BaseEntity
{
    /// <summary>
    /// Gets or sets the current status of the plan based on its lifecycle.
    /// </summary>
    public PlanType Status { get; set; }

    /// <summary>
    /// Gets or sets the result of the plan execution.
    /// Contains request-response pairs captured during execution.
    /// </summary>
    public PlanResultValueObject Result { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of tasks to be executed as part of this plan.
    /// Each plan contains one or more tasks that define the execution strategy.
    /// </summary>
    public IEnumerable<TaskValueObject> Tasks { get; set; } = null!;
}