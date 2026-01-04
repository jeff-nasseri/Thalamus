using Application.Common.Dtos;

namespace Application.Common.Factories.Agent;

/// <summary>
///     Abstract factory for creating agents in the Thalamus system.
///     Provides a template for agent creation implementations.
/// </summary>
public abstract class AgentFactory
{
    /// <summary>
    ///     Creates a new agent asynchronously based on the provided configuration.
    /// </summary>
    /// <param name="dto">The agent installation configuration containing setup parameters.</param>
    /// <param name="token">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the created agent DTO.</returns>
    public abstract Task<AgentDto> CreateAgentAsync(AgentInstallationConfigurationDto dto, CancellationToken token);
}