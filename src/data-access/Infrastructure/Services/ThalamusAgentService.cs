using Application.Common.Dtos;
using Application.Services.Agent;

namespace Infrastructure.Services;

/// <summary>
///     Implementation of the agent service for the Thalamus platform.
///     Manages agent lifecycle operations including creation, registration, and state management.
/// </summary>
public class ThalamusAgentService : IAgentService
{
    /// <summary>
    ///     Starts an agent with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to start.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task StartAgentAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Stops an agent with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to stop.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task StopAgentAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Disables an agent with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to disable.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task DisableAgentAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Retrieves information about an agent.
    /// </summary>
    /// <param name="id">The unique identifier of the agent.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the agent information.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task<AgentDto> GetAgentInformationAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Registers an agent in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to register.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task RegisterAgentAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Creates a new agent based on the provided configuration.
    /// </summary>
    /// <param name="dto">The agent installation configuration.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the created agent information.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task<AgentDto> CreateNewAgentAsync(AgentInstallationConfigurationDto dto,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}