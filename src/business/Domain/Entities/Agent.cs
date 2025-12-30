using Domain.Common.BaseTypes;

namespace Domain.Entities;

/// <summary>
/// Represents an agent in the Thalamus system.
/// Agents execute plans and maintain their own memory of conversations and executions.
/// </summary>
public class Agent : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the agent.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of memories associated with this agent.
    /// Each memory contains the agent's conversation history and execution records.
    /// </summary>
    public IEnumerable<Memory> Memories { get; set; } = null!;
}