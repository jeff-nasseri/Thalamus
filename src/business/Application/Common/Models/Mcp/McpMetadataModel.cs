using System.Text.Json.Serialization;

namespace Application.Common.Models.Mcp;

/// <summary>
///     Metadata information for MCP configuration.
/// </summary>
public class McpMetadataModel
{
	/// <summary>
	///     Gets or sets the creation timestamp.
	/// </summary>
	[JsonPropertyName("created")]
    public DateTime Created { get; set; }

	/// <summary>
	///     Gets or sets the environment (production, development, staging).
	/// </summary>
	[JsonPropertyName("environment")]
    public string Environment { get; set; } = null!;

	/// <summary>
	///     Gets or sets the organization name.
	/// </summary>
	[JsonPropertyName("organization")]
    public string Organization { get; set; } = null!;
}