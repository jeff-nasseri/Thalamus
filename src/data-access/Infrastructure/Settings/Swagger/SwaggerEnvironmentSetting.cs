using Application.Settings.Environments;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.Swagger;

/// <summary>
///     Configuration settings for Swagger UI authentication.
/// </summary>
public class SwaggerEnvironmentSetting : EnvironmentSetting
{
    /// <summary>
    ///     Gets or sets the username for Swagger UI basic authentication.
    /// </summary>
    [ConfigurationKeyName("SWAGGER_USERNAME")]
    public string? Username { get; set; }

    /// <summary>
    ///     Gets or sets the password for Swagger UI basic authentication.
    /// </summary>
    [ConfigurationKeyName("SWAGGER_PASSWORD")]
    public string? Password { get; set; }
}