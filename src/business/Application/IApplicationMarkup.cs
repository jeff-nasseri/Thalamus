using Domain;
using Domain.Common.BaseTypes;

namespace Application;

/// <summary>
///     Marker interface for Application layer domain markup.
///     Inherits from <see cref="IDomainMarkup" /> to identify Application layer types.
/// </summary>
public interface IApplicationMarkup : IMarkup
{
}