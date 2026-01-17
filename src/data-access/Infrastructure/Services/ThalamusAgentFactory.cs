using Application.Common.Dtos;
using Application.Common.Factories.Agent;

namespace Infrastructure.Services;

/// <summary>
///     Factory implementation for creating Thalamus-specific agents.
///     Extends the base AgentFactory with Thalamus platform capabilities.
/// </summary>
public abstract class ThalamusAgentFactory : AgentFactory, IAgentFactory
{
    /// <summary>
    ///     Gets the factory tag identifier.
    /// </summary>
    public string Tag => nameof(ThalamusAgentFactory);

    /// <summary>
    ///     Creates a new Thalamus agent asynchronously based on the provided configuration.
    /// </summary>
    /// <param name="dto">The agent installation configuration containing setup parameters.</param>
    /// <param name="token">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the created agent DTO.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public override Task<AgentDto> CreateAgentAsync(AgentInstallationConfigurationDto dto, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}