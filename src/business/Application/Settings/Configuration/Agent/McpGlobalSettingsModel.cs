using System.Text.Json.Serialization;

namespace Application.Settings.Configuration.Agent;

/// <summary>
///     Global settings applicable to all MCPs.
/// </summary>
public class McpGlobalSettingsModel
{
	/// <summary>
	///     Gets or sets the logging level.
	/// </summary>
	[JsonPropertyName("log_level")]
    public string LogLevel { get; set; } = null!;

	/// <summary>
	///     Gets or sets the log format (json, text).
	/// </summary>
	[JsonPropertyName("log_format")]
    public string LogFormat { get; set; } = null!;

	/// <summary>
	///     Gets or sets whether telemetry is enabled.
	/// </summary>
	[JsonPropertyName("enable_telemetry")]
    public bool EnableTelemetry { get; set; }

	/// <summary>
	///     Gets or sets the health check interval in seconds.
	/// </summary>
	[JsonPropertyName("health_check_interval")]
    public int HealthCheckInterval { get; set; }

	/// <summary>
	///     Gets or sets the graceful shutdown timeout in seconds.
	/// </summary>
	[JsonPropertyName("graceful_shutdown_timeout")]
    public int GracefulShutdownTimeout { get; set; }

	/// <summary>
	///     Gets or sets the maximum retry attempts.
	/// </summary>
	[JsonPropertyName("max_retry_attempts")]
    public int MaxRetryAttempts { get; set; }

	/// <summary>
	///     Gets or sets the retry backoff strategy.
	/// </summary>
	[JsonPropertyName("retry_backoff")]
    public string RetryBackoff { get; set; } = null!;
}