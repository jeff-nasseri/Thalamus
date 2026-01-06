using System.Text.Json.Serialization;
using Application.Common.Models.Configuration;

namespace Application.Common.Models.Mcp;

/// <summary>
///     Security configuration settings for MCPs.
/// </summary>
public class McpSecuritySettingsModel
{
	/// <summary>
	///     Gets or sets whether TLS is enabled.
	/// </summary>
	[JsonPropertyName("enable_tls")]
    public bool EnableTls { get; set; }

	/// <summary>
	///     Gets or sets the allowed CORS origins.
	/// </summary>
	[JsonPropertyName("cors_allowed_origins")]
    public string CorsAllowedOrigins { get; set; } = null!;

	/// <summary>
	///     Gets or sets the rate limiting configuration.
	/// </summary>
	[JsonPropertyName("rate_limiting")]
    public RateLimitingConfigModel RateLimiting { get; set; } = null!;
}