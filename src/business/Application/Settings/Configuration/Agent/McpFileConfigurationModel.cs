using System.Text.Json.Serialization;

namespace Application.Settings.Configuration.Agent;

/// <summary>
///     Configuration model for MCP (Model Context Protocol) pool settings.
///     Maps to the mcp-pool.json configuration file structure.
/// </summary>
public class McpFileConfigurationModel : FileConfigurationModel
{
	/// <summary>
	///     Gets the configuration model type.
	/// </summary>
	public override ConfigurationModelType ConfigurationModelType { get; } = ConfigurationModelType.SUPPORTED_MCP_JSON;

	/// <summary>
	///     Gets or sets the configuration version.
	/// </summary>
	[JsonPropertyName("version")]
    public string Version { get; set; } = null!;

	/// <summary>
	///     Gets or sets the metadata about the configuration.
	/// </summary>
	[JsonPropertyName("metadata")]
    public McpMetadataModel Metadata { get; set; } = null!;

	/// <summary>
	///     Gets or sets the collection of required MCP plugins.
	/// </summary>
	[JsonPropertyName("required_mcps")]
    public List<McpDefinitionModel> RequiredMcps { get; set; } = new();

	/// <summary>
	///     Gets or sets the global settings for all MCPs.
	/// </summary>
	[JsonPropertyName("global_settings")]
    public McpGlobalSettingsModel GlobalSettings { get; set; } = null!;

	/// <summary>
	///     Gets or sets the network configuration settings.
	/// </summary>
	[JsonPropertyName("network_settings")]
    public McpNetworkSettingsModel NetworkSettings { get; set; } = null!;

	/// <summary>
	///     Gets or sets the security configuration settings.
	/// </summary>
	[JsonPropertyName("security_settings")]
    public McpSecuritySettingsModel SecuritySettings { get; set; } = null!;
}