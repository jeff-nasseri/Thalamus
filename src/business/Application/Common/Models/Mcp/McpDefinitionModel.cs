using System.Text.Json.Serialization;

namespace Application.Common.Models.Mcp;

/// <summary>
///     Definition of an MCP plugin with its configurations.
/// </summary>
public class McpDefinitionModel
{
	/// <summary>
	///     Gets or sets the display title of the MCP.
	/// </summary>
	[JsonPropertyName("title")]
    public string Title { get; set; } = null!;

	/// <summary>
	///     Gets or sets the unique code identifier for the MCP.
	/// </summary>
	[JsonPropertyName("mcp_code")]
    public string McpCode { get; set; } = null!;

	/// <summary>
	///     Gets or sets the description of the MCP functionality.
	/// </summary>
	[JsonPropertyName("description")]
    public string Description { get; set; } = null!;

	/// <summary>
	///     Gets or sets whether this MCP is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

	/// <summary>
	///     Gets or sets the collection of MCP configurations for different platforms.
	/// </summary>
	[JsonPropertyName("mcp_configurations")]
    public List<McpConfigurationModel> McpConfigurations { get; set; } = new();
}