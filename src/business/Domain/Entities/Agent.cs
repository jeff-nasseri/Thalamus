using Domain.Common.BaseTypes;

namespace Domain.Entities;

/// <summary>
///     Represents an agent in the Thalamus system.
///     Agents execute plans and maintain their own memory of conversations and executions.
/// </summary>
public class Agent : BaseEntity
{
    /// <summary>
    ///     Gets or sets the name of the agent.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the collection of memories associated with this agent.
    ///     Each memory contains the agent's conversation history and execution records.
    /// </summary>
    public IEnumerable<Memory> Memories { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the collection of Model Context Protocol (MCP) plugins available to this agent.
    ///     Plugins extend the agent's capabilities with additional tools and integrations.
    /// </summary>
    public IEnumerable<McpPlugin>? Plugins { get; set; }

    /// <summary>
    ///     Gets or sets the current operational status of the agent.
    /// </summary>
    public AgentStatus Status { get; set; }
}