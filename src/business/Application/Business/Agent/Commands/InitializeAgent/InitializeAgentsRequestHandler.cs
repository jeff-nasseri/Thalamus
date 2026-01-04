using Application.Common.Dtos;
using ErrorHandling;
using MediatR;

namespace Application.Business.Agent.Commands.InitializeAgent;

/// <summary>
///     Handles the initialization of multiple agents in the system.
///     Processes agent initialization requests and coordinates agent creation.
/// </summary>
public class InitializeAgentsRequestHandler : IRequestHandler<InitializeAgentsRequest, Response<IEnumerable<AgentDto>>>
{
    /// <summary>
    ///     Handles the agent initialization request.
    /// </summary>
    /// <param name="request">The initialization request containing configuration data.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the response with initialized agents.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    public Task<Response<IEnumerable<AgentDto>>> Handle(InitializeAgentsRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}