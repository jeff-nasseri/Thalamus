using Application.Common.Dtos;
using ErrorHandling;
using MediatR;

namespace Application.Business.Agent.Commands.InitializeAgent;

/// <summary>
///     Request to initialize multiple agents in the system.
/// </summary>
/// <param name="Dto">The initialization configuration data.</param>
public record InitializeAgentsRequest(InitializeAgentsRequestDto Dto) : IRequest<Response<IEnumerable<AgentDto>>>;