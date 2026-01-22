using Application.Common.Attributes;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Storage.Repository.Contracts;
using AutoMapper;
using Domain.Entities;
using ErrorHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Business.Node.Commands.InitializeNode;

/// <summary>
///     Handles the initialization of multiple nodes in the system.
///     Processes node initialization requests and coordinates node creation.
/// </summary>
[IdempotentHandler]
public class InitializeNodeRequestHandler : IRequestHandler<InitializeNodeRequest, Response<IEnumerable<NodeDto>>>
{
    private readonly ILogger<InitializeNodeRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEntityRepository<AgentNode, Guid> _nodeRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="InitializeNodeRequestHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger for structured logging.</param>
    /// <param name="nodeRepository">The repository for node persistence operations.</param>
    /// <param name="mapper">AutoMapper instance for DTO to entity mapping.</param>
    public InitializeNodeRequestHandler(
        ILogger<InitializeNodeRequestHandler> logger,
        IEntityRepository<AgentNode, Guid> nodeRepository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _nodeRepository = nodeRepository ?? throw new ArgumentNullException(nameof(nodeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Gets or sets the current handler request being processed.
    /// </summary>
    public required InitializeNodeRequest HandlerRequest { get; set; }

    /// <summary>
    ///     Gets or sets the collection of mapped node entities.
    /// </summary>
    public IEnumerable<AgentNode>? MappedNodes { get; set; }

    /// <summary>
    ///     Handles the node initialization request.
    /// </summary>
    /// <param name="request">The initialization request containing configuration data.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the response with initialized nodes.</returns>
    public async Task<Response<IEnumerable<NodeDto>>> Handle(InitializeNodeRequest request,
        CancellationToken cancellationToken)
    {
        HandlerRequest = request;

        if ((await MapNodesFromDtos()).TryGetError(out var mappingError)) return mappingError!.Value;

        if ((await InsertNodesToRepository()).TryGetError(out var insertError)) return insertError!.Value;

        return MapNodesToResponse();
    }

    /// <summary>
    ///     Maps node DTOs to domain entities using AutoMapper.
    /// </summary>
    /// <returns>A response indicating success or failure of the mapping operation.</returns>
    private async Task<Response> MapNodesFromDtos()
    {
        try
        {
            MappedNodes = _mapper.Map<IEnumerable<AgentNode>>(HandlerRequest.Dto.Nodes);

            if (MappedNodes != null && MappedNodes.Any()) return Response.Successful();

            var error = InitializeNodeErrorCodes.NodeMappingFailed;
            _logger.LogError(this, error,
                "NODE_INITIALIZATION.MAPPING.ERROR --- Failed to map nodes from DTOs. Node count: {count}",
                HandlerRequest.Dto.Nodes?.Count() ?? 0);

            return error;
        }
        catch (AutoMapperMappingException exception)
        {
            var error = InitializeNodeErrorCodes.NodeMappingFailed;
            _logger.LogError(exception, this, error,
                "NODE_INITIALIZATION.MAPPING.EXCEPTION --- AutoMapper exception during node mapping");

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeNodeErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "NODE_INITIALIZATION.MAPPING.UNKNOWN --- Unexpected exception during node mapping");

            return error;
        }
    }

    /// <summary>
    ///     Inserts the mapped node entities into the repository.
    /// </summary>
    /// <returns>A response indicating success or failure of the insertion operation.</returns>
    private async Task<Response> InsertNodesToRepository()
    {
        try
        {
            await _nodeRepository.InsertRange(MappedNodes!);
            return Response.Successful();
        }
        catch (DbUpdateException exception)
        {
            var error = InitializeNodeErrorCodes.NodeInsertFailed;
            _logger.LogError(exception, this, error,
                "NODE_INITIALIZATION.INSERT.DB_EXCEPTION --- Database exception during node insertion. Node count: {count}",
                MappedNodes?.Count() ?? 0);

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeNodeErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "NODE_INITIALIZATION.INSERT.UNKNOWN --- Unexpected exception during node insertion");

            return error;
        }
    }

    /// <summary>
    ///     Maps the domain node entities back to DTOs for the response.
    /// </summary>
    /// <returns>A response containing the collection of node DTOs.</returns>
    private Response<IEnumerable<NodeDto>> MapNodesToResponse()
    {
        try
        {
            var nodeDtos = _mapper.Map<IEnumerable<NodeDto>>(MappedNodes);
            return (Response<IEnumerable<NodeDto>>)nodeDtos;
        }
        catch (AutoMapperMappingException exception)
        {
            var error = InitializeNodeErrorCodes.NodeMappingFailed;
            _logger.LogError(exception, this, error,
                "NODE_INITIALIZATION.RESPONSE_MAPPING.EXCEPTION --- Failed to map nodes to DTOs for response");

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeNodeErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "NODE_INITIALIZATION.RESPONSE_MAPPING.UNKNOWN --- Unexpected exception during response mapping");

            return error;
        }
    }
}