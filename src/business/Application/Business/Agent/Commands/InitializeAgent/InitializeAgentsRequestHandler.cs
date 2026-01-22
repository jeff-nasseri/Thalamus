using Application.Common.Attributes;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Storage.Repository.Contracts;
using AutoMapper;
using ErrorHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Business.Agent.Commands.InitializeAgent;

/// <summary>
///     Handles the initialization of multiple agents in the system.
///     Processes agent initialization requests and coordinates agent creation.
/// </summary>
[IdempotentHandler]
public class InitializeAgentsRequestHandler : IRequestHandler<InitializeAgentsRequest, Response<IEnumerable<AgentDto>>>
{
    private readonly IEntityRepository<Domain.Entities.Agent, Guid> _agentRepository;
    private readonly ILogger<InitializeAgentsRequestHandler> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="InitializeAgentsRequestHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger for structured logging.</param>
    /// <param name="agentRepository">The repository for agent persistence operations.</param>
    /// <param name="mapper">AutoMapper instance for DTO to entity mapping.</param>
    public InitializeAgentsRequestHandler(
        ILogger<InitializeAgentsRequestHandler> logger,
        IEntityRepository<Domain.Entities.Agent, Guid> agentRepository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _agentRepository = agentRepository ?? throw new ArgumentNullException(nameof(agentRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Gets or sets the current handler request being processed.
    /// </summary>
    public required InitializeAgentsRequest HandlerRequest { get; set; }

    /// <summary>
    ///     Gets or sets the collection of mapped agent entities.
    /// </summary>
    public IEnumerable<Domain.Entities.Agent>? MappedAgents { get; set; }

    /// <summary>
    ///     Handles the agent initialization request.
    /// </summary>
    /// <param name="request">The initialization request containing configuration data.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the response with initialized agents.</returns>
    public async Task<Response<IEnumerable<AgentDto>>> Handle(InitializeAgentsRequest request,
        CancellationToken cancellationToken)
    {
        HandlerRequest = request;

        if ((await MapAgentsFromDtos()).TryGetError(out var mappingError)) return mappingError!.Value;

        if ((await InsertAgentsToRepository()).TryGetError(out var insertError)) return insertError!.Value;

        return MapAgentsToResponse();
    }

    /// <summary>
    ///     Maps agent DTOs to domain entities using AutoMapper.
    /// </summary>
    /// <returns>A response indicating success or failure of the mapping operation.</returns>
    private async Task<Response> MapAgentsFromDtos()
    {
        try
        {
            MappedAgents = _mapper.Map<IEnumerable<Domain.Entities.Agent>>(HandlerRequest.Dto.Agents);

            if (MappedAgents != null && MappedAgents.Any()) return Response.Successful();

            var error = InitializeAgentsErrorCodes.AgentMappingFailed;
            _logger.LogError(this, error,
                "AGENT_INITIALIZATION.MAPPING.ERROR --- Failed to map agents from DTOs. Agent count: {count}",
                HandlerRequest.Dto.Agents?.Count() ?? 0);

            return error;
        }
        catch (AutoMapperMappingException exception)
        {
            var error = InitializeAgentsErrorCodes.AgentMappingFailed;
            _logger.LogError(exception, this, error,
                "AGENT_INITIALIZATION.MAPPING.EXCEPTION --- AutoMapper exception during agent mapping");

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeAgentsErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "AGENT_INITIALIZATION.MAPPING.UNKNOWN --- Unexpected exception during agent mapping");

            return error;
        }
    }

    /// <summary>
    ///     Inserts the mapped agent entities into the repository.
    /// </summary>
    /// <returns>A response indicating success or failure of the insertion operation.</returns>
    private async Task<Response> InsertAgentsToRepository()
    {
        try
        {
            await _agentRepository.InsertRange(MappedAgents!);
            return Response.Successful();
        }
        catch (DbUpdateException exception)
        {
            var error = InitializeAgentsErrorCodes.AgentInsertFailed;
            _logger.LogError(exception, this, error,
                "AGENT_INITIALIZATION.INSERT.DB_EXCEPTION --- Database exception during agent insertion. Agent count: {count}",
                MappedAgents?.Count() ?? 0);

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeAgentsErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "AGENT_INITIALIZATION.INSERT.UNKNOWN --- Unexpected exception during agent insertion");

            return error;
        }
    }

    /// <summary>
    ///     Maps the domain agent entities back to DTOs for the response.
    /// </summary>
    /// <returns>A response containing the collection of agent DTOs.</returns>
    private Response<IEnumerable<AgentDto>> MapAgentsToResponse()
    {
        try
        {
            var agentDtos = _mapper.Map<IEnumerable<AgentDto>>(MappedAgents);
            return (Response<IEnumerable<AgentDto>>)agentDtos;
        }
        catch (AutoMapperMappingException exception)
        {
            var error = InitializeAgentsErrorCodes.AgentMappingFailed;
            _logger.LogError(exception, this, error,
                "AGENT_INITIALIZATION.RESPONSE_MAPPING.EXCEPTION --- Failed to map agents to DTOs for response");

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeAgentsErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "AGENT_INITIALIZATION.RESPONSE_MAPPING.UNKNOWN --- Unexpected exception during response mapping");

            return error;
        }
    }
}