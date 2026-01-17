using Application.Common.Dtos;

namespace Application.Business.Agent.Commands.InitializeAgent;

/// <summary>
///     Data transfer object for agent initialization requests.
/// </summary>
/// <param name="Agents">The collection of agents to initialize.</param>
public record InitializeAgentsRequestDto(IEnumerable<AgentDto> Agents);