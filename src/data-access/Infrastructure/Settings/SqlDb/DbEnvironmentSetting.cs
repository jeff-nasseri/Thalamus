using Application.Settings.Environments;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.SqlDb;

/// <summary>
///     Configuration settings for database connection and initialization.
/// </summary>
public sealed class DbEnvironmentSetting : EnvironmentSetting
{
    /// <summary>
    ///     Gets or sets the database host address.
    /// </summary>
    [ConfigurationKeyName("DB_APP_HOST")]
    public string? DbAppHost { get; set; }

    /// <summary>
    ///     Gets or sets the database port number.
    /// </summary>
    [ConfigurationKeyName("DB_APP_PORT")]
    public string? DbAppPort { get; set; }

    /// <summary>
    ///     Gets or sets the database name.
    /// </summary>
    [ConfigurationKeyName("DB_APP_NAME")]
    public string? DbAppName { get; set; }

    /// <summary>
    ///     Gets or sets the database username for authentication.
    /// </summary>
    [ConfigurationKeyName("DB_APP_USER")]
    public string? DbAppUser { get; set; }

    /// <summary>
    ///     Gets or sets the database password for authentication.
    /// </summary>
    [ConfigurationKeyName("DB_APP_PASS")]
    public string? DbAppPass { get; set; }

    /// <summary>
    ///     Gets or sets the database version.
    /// </summary>
    [ConfigurationKeyName("DB_APP_VERSION")]
    public string? DbAppVersion { get; set; }
}