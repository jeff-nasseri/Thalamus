using Application.Common.Dtos;
using ErrorHandling;
using MediatR;

namespace Application.Business.Node.Commands.InitializeNode;

/// <summary>
///     Request to initialize multiple nodes in the system.
/// </summary>
/// <param name="Dto">The initialization configuration data.</param>
public record InitializeNodeRequest(InitializeNodeRequestDto Dto) : IRequest<Response<IEnumerable<NodeDto>>>;