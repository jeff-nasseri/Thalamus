using Application.Common.Dtos;
using ErrorHandling;
using MediatR;

namespace Application.Business.Mcp.Commands.InitializeMcp;

/// <summary>
///     Request to initialize multiple MCP plugins in the system.
/// </summary>
/// <param name="Dto">The initialization configuration data.</param>
public record InitializeMcpRequest(InitializeMcpRequestDto Dto) : IRequest<Response<IEnumerable<McpPluginDto>>>;