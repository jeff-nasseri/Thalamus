using Application.Common.BaseTypes;
using Application.Common.Dtos;

namespace Application.Services.Agent;

/// <summary>
///     Interface for agent-specific configuration management.
///     Handles loading and parsing of agent installation configurations from files.
/// </summary>
public interface IAgentConfiguration : IFileConfiguration
{
    /// <summary>
    ///     Generates agent installation configurations from a configuration file.
    /// </summary>
    /// <param name="path">The path to the configuration file.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the collection of installation configurations.</returns>
    Task<IEnumerable<AgentInstallationConfigurationDto>> GenerateInstallationConfigurationFromConfigurationFileAsync(
        string path,
        CancellationToken cancellationToken);
}