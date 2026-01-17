using Domain.Entities;

namespace Application.Common.Dtos;

/// <summary>
///     Data transfer object for agent information.
/// </summary>
/// <param name="Name">The name of the agent.</param>
/// <param name="Status">The current operational status of the agent.</param>
/// <param name="Plugins">The collection of MCP plugins available to this agent.</param>
public record AgentDto(
    string Name,
    AgentStatus Status = AgentStatus.STOPPED,
    IEnumerable<McpPluginDto>? Plugins = null
);