using Application.Common.Dtos;
using Application.Services.Agent;

namespace Infrastructure;

/// <summary>
/// Implementation of agent configuration management.
/// Loads agent pool configurations from the agent-pool configuration file.
/// </summary>
public class AgentConfiguration : IAgentConfiguration
{
    /// <summary>
    /// Gets the configuration key for the agent pool configuration.
    /// </summary>
    public string ConfigurationKey => "agent-pool";

    /// <summary>
    /// Generates agent installation configurations from a configuration file.
    /// </summary>
    /// <param name="path">The path to the configuration file.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the collection of installation configurations.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task<IEnumerable<AgentInstallationConfigurationDto>> GenerateInstallationConfigurationFromConfigurationFileAsync(
        string path,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}