using Application.Common.Dtos;

namespace Application.Business.Mcp.Commands.InitializeMcp;

/// <summary>
///     Data transfer object for MCP plugin initialization requests.
/// </summary>
/// <param name="Plugins">The collection of MCP plugins to initialize.</param>
public record InitializeMcpRequestDto(IEnumerable<McpPluginDto> Plugins);