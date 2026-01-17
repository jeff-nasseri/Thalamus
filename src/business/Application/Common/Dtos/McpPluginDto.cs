namespace Application.Common.Dtos;

/// <summary>
///     Data transfer object for Model Context Protocol (MCP) plugin configuration.
/// </summary>
/// <param name="Title">The display title of the plugin.</param>
/// <param name="Code">The unique code identifier for the plugin.</param>
/// <param name="Description">A description of the plugin's functionality and purpose.</param>
/// <param name="Enabled">Whether the plugin is enabled for use.</param>
/// <param name="Platform">The runtime platform (e.g., docker, process, kubernetes).</param>
/// <param name="ImageName">The Docker image name (when Platform is docker).</param>
/// <param name="ContainerName">The container name (when Platform is docker).</param>
/// <param name="RestartPolicy">The restart policy for the MCP.</param>
/// <param name="Environment">The environment variables for the MCP runtime.</param>
/// <param name="Ports">The port mappings for the MCP (host:container format).</param>
/// <param name="Volumes">The volume mappings for the MCP (host:container format).</param>
/// <param name="Networks">The network names to connect the MCP to.</param>
/// <param name="Configuration">Platform-specific configuration dictionary.</param>
public record McpPluginDto(
    string Title,
    string Code,
    string Description,
    bool Enabled = true,
    string Platform = "docker",
    string? ImageName = null,
    string? ContainerName = null,
    string? RestartPolicy = null,
    Dictionary<string, string>? Environment = null,
    List<string>? Ports = null,
    List<string>? Volumes = null,
    List<string>? Networks = null,
    Dictionary<string, object>? Configuration = null
);