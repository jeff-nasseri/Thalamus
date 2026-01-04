namespace Application.Common.BaseTypes;

/// <summary>
///     Interface for file-based configuration providers.
///     Defines configuration that is loaded from external files.
/// </summary>
public interface IFileConfiguration : IConfiguration
{
    /// <summary>
    ///     Gets the unique key identifying this configuration in the configuration file.
    /// </summary>
    string ConfigurationKey { get; }
}