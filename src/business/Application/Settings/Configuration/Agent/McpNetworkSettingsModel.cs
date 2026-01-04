using System.Text.Json.Serialization;

namespace Application.Settings.Configuration.Agent;

/// <summary>
///     Network configuration settings for MCPs.
/// </summary>
public class McpNetworkSettingsModel
{
	/// <summary>
	///     Gets or sets the Docker network configuration.
	/// </summary>
	[JsonPropertyName("docker")]
    public DockerNetworkConfigModel Docker { get; set; } = null!;
}