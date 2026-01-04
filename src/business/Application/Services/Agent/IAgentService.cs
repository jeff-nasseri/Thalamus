using Application.Common.Dtos;

namespace Application.Services.Agent;

/// <summary>
///     Service interface for managing agent lifecycle and operations.
///     Provides methods for starting, stopping, and managing agents.
/// </summary>
public interface IAgentService
{
    /// <summary>
    ///     Starts an agent with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to start.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task StartAgentAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Stops an agent with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to stop.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task StopAgentAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Disables an agent with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to disable.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DisableAgentAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Retrieves information about an agent.
    /// </summary>
    /// <param name="id">The unique identifier of the agent.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the agent information.</returns>
    Task<AgentDto> GetAgentInformationAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Registers an agent in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the agent to register.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RegisterAgentAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Creates a new agent based on the provided configuration.
    /// </summary>
    /// <param name="dto">The agent installation configuration.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the created agent information.</returns>
    Task<AgentDto> CreateNewAgentAsync(AgentInstallationConfigurationDto dto, CancellationToken cancellationToken);
}