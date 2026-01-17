using Application.Common.Dtos;

namespace Application.Business.Node.Commands.InitializeNode;

/// <summary>
///     Data transfer object for node initialization requests.
/// </summary>
/// <param name="Nodes">The collection of nodes to initialize.</param>
public record InitializeNodeRequestDto(IEnumerable<NodeDto> Nodes);