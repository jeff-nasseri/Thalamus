using System.Text.Json.Serialization;

namespace Application.Settings.Configuration.Agent;

/// <summary>
///     Docker network configuration.
/// </summary>
public class DockerNetworkConfigModel
{
	/// <summary>
	///     Gets or sets the network name.
	/// </summary>
	[JsonPropertyName("network_name")]
    public string NetworkName { get; set; } = null!;

	/// <summary>
	///     Gets or sets the network driver.
	/// </summary>
	[JsonPropertyName("driver")]
    public string Driver { get; set; } = null!;

	/// <summary>
	///     Gets or sets the subnet CIDR.
	/// </summary>
	[JsonPropertyName("subnet")]
    public string Subnet { get; set; } = null!;
}