using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common.Settings;

/// <summary>
/// Configuration settings for Sentry error tracking and monitoring.
/// </summary>
/// <remarks>
/// This class is currently commented out and not in use.
/// Uncomment and configure when Sentry integration is required.
/// </remarks>
public class SentryEnvironmentSetting
{
    /// <summary>
    /// Gets or sets the Sentry Data Source Name (DSN) for error reporting.
    /// </summary>
    [ConfigurationKeyName("SENTRY_DSN")]
    public string? Dsn { get; set; }

    /// <summary>
    /// Gets or sets the environment name for Sentry (e.g., Development, Staging, Production).
    /// </summary>
    [ConfigurationKeyName("SENTRY_ENVIRONMENT")]
    public string? Environment { get; set; }
}
