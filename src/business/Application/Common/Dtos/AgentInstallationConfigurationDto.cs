namespace Application.Common.Dtos;

/// <summary>
///     Data transfer object containing agent installation configuration.
/// </summary>
/// <param name="Name">The name of the agent to install.</param>
/// <param name="dtos">Collection of MCP plugin configurations for the agent.</param>
public record AgentInstallationConfigurationDto(string Name, IEnumerable<McpPluginDto> dtos);