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

namespace Application.Business.Mcp.Commands.InitializeMcp;

/// <summary>
///     Handles the initialization of multiple MCP plugins in the system.
///     Processes MCP plugin initialization requests and coordinates plugin creation.
/// </summary>
[IdempotentHandler]
public class InitializeMcpRequestHandler : IRequestHandler<InitializeMcpRequest, Response<IEnumerable<McpPluginDto>>>
{
    private readonly ILogger<InitializeMcpRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEntityRepository<McpPlugin, Guid> _mcpPluginRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="InitializeMcpRequestHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger for structured logging.</param>
    /// <param name="mcpPluginRepository">The repository for MCP plugin persistence operations.</param>
    /// <param name="mapper">AutoMapper instance for DTO to entity mapping.</param>
    public InitializeMcpRequestHandler(
        ILogger<InitializeMcpRequestHandler> logger,
        IEntityRepository<McpPlugin, Guid> mcpPluginRepository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mcpPluginRepository = mcpPluginRepository ?? throw new ArgumentNullException(nameof(mcpPluginRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    ///     Gets or sets the current handler request being processed.
    /// </summary>
    public required InitializeMcpRequest HandlerRequest { get; set; }

    /// <summary>
    ///     Gets or sets the collection of mapped MCP plugin entities.
    /// </summary>
    public IEnumerable<McpPlugin>? MappedPlugins { get; set; }

    /// <summary>
    ///     Handles the MCP plugin initialization request.
    /// </summary>
    /// <param name="request">The initialization request containing configuration data.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the response with initialized MCP plugins.</returns>
    public async Task<Response<IEnumerable<McpPluginDto>>> Handle(InitializeMcpRequest request,
        CancellationToken cancellationToken)
    {
        HandlerRequest = request;

        if ((await MapPluginsFromDtos()).TryGetError(out var mappingError)) return mappingError!.Value;

        if ((await InsertPluginsToRepository()).TryGetError(out var insertError)) return insertError!.Value;

        return MapPluginsToResponse();
    }

    /// <summary>
    ///     Maps MCP plugin DTOs to domain entities using AutoMapper.
    /// </summary>
    /// <returns>A response indicating success or failure of the mapping operation.</returns>
    private async Task<Response> MapPluginsFromDtos()
    {
        try
        {
            MappedPlugins = _mapper.Map<IEnumerable<McpPlugin>>(HandlerRequest.Dto.Plugins);

            if (MappedPlugins != null && MappedPlugins.Any()) return Response.Successful();

            var error = InitializeMcpErrorCodes.McpMappingFailed;
            _logger.LogError(this, error,
                "MCP_INITIALIZATION.MAPPING.ERROR --- Failed to map MCP plugins from DTOs. Plugin count: {count}",
                HandlerRequest.Dto.Plugins?.Count() ?? 0);

            return error;
        }
        catch (AutoMapperMappingException exception)
        {
            var error = InitializeMcpErrorCodes.McpMappingFailed;
            _logger.LogError(exception, this, error,
                "MCP_INITIALIZATION.MAPPING.EXCEPTION --- AutoMapper exception during MCP plugin mapping");

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeMcpErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "MCP_INITIALIZATION.MAPPING.UNKNOWN --- Unexpected exception during MCP plugin mapping");

            return error;
        }
    }

    /// <summary>
    ///     Inserts the mapped MCP plugin entities into the repository.
    /// </summary>
    /// <returns>A response indicating success or failure of the insertion operation.</returns>
    private async Task<Response> InsertPluginsToRepository()
    {
        try
        {
            await _mcpPluginRepository.InsertRange(MappedPlugins!);
            return Response.Successful();
        }
        catch (DbUpdateException exception)
        {
            var error = InitializeMcpErrorCodes.McpInsertFailed;
            _logger.LogError(exception, this, error,
                "MCP_INITIALIZATION.INSERT.DB_EXCEPTION --- Database exception during MCP plugin insertion. Plugin count: {count}",
                MappedPlugins?.Count() ?? 0);

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeMcpErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "MCP_INITIALIZATION.INSERT.UNKNOWN --- Unexpected exception during MCP plugin insertion");

            return error;
        }
    }

    /// <summary>
    ///     Maps the domain MCP plugin entities back to DTOs for the response.
    /// </summary>
    /// <returns>A response containing the collection of MCP plugin DTOs.</returns>
    private Response<IEnumerable<McpPluginDto>> MapPluginsToResponse()
    {
        try
        {
            var pluginDtos = _mapper.Map<IEnumerable<McpPluginDto>>(MappedPlugins);
            return (Response<IEnumerable<McpPluginDto>>)pluginDtos;
        }
        catch (AutoMapperMappingException exception)
        {
            var error = InitializeMcpErrorCodes.McpMappingFailed;
            _logger.LogError(exception, this, error,
                "MCP_INITIALIZATION.RESPONSE_MAPPING.EXCEPTION --- Failed to map MCP plugins to DTOs for response");

            return error;
        }
        catch (Exception exception)
        {
            var error = InitializeMcpErrorCodes.UnknownInitializationError;
            _logger.LogError(exception, this, error,
                "MCP_INITIALIZATION.RESPONSE_MAPPING.UNKNOWN --- Unexpected exception during response mapping");

            return error;
        }
    }
}