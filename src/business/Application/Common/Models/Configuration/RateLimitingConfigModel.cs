using System.Text.Json.Serialization;

namespace Application.Common.Models.Configuration;

/// <summary>
///     Rate limiting configuration.
/// </summary>
public class RateLimitingConfigModel
{
	/// <summary>
	///     Gets or sets whether rate limiting is enabled.
	/// </summary>
	[JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

	/// <summary>
	///     Gets or sets the maximum requests per minute.
	/// </summary>
	[JsonPropertyName("requests_per_minute")]
    public int RequestsPerMinute { get; set; }

	/// <summary>
	///     Gets or sets the burst size.
	/// </summary>
	[JsonPropertyName("burst_size")]
    public int BurstSize { get; set; }
}