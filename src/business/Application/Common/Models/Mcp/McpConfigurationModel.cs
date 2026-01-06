using System.Text.Json.Serialization;

namespace Application.Common.Models.Mcp;

/// <summary>
///     Platform-specific configuration for running an MCP.
/// </summary>
public class McpConfigurationModel
{
	/// <summary>
	///     Gets or sets the runtime platform (e.g., "docker", "kubernetes").
	/// </summary>
	[JsonPropertyName("platform")]
    public string Platform { get; set; } = null!;

	/// <summary>
	///     Gets or sets the platform-specific configuration details.
	/// </summary>
	[JsonPropertyName("configuration")]
    public Dictionary<string, object> Configuration { get; set; } = new();
}