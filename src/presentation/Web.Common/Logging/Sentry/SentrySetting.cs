using Microsoft.Extensions.Configuration;

namespace Thalamus.Web.Logging.Sentry;

/// <summary>
/// Configuration settings for Sentry integration.
/// </summary>
public class SentrySetting
{
    /// <summary>
    /// Gets or sets the Sentry Data Source Name (DSN) for error reporting.
    /// </summary>
    [ConfigurationKeyName("SENTRY_DSN")]
    public string? Dsn { get; set; }

    /// <summary>
    /// Gets or sets the environment name for Sentry (e.g., "production", "staging").
    /// </summary>
    [ConfigurationKeyName("SENTRY_ENVIRONMENT")]
    public string? Environment { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Sentry logging is active.
    /// Default value is true.
    /// </summary>
    [ConfigurationKeyName("SENTRY_IS_ACTIVE")]
    public bool IsActive { get; set; } = true;
}