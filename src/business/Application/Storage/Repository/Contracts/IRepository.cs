using Application.Storage.Repository.Contracts.Availability;
using Application.Storage.Repository.Contracts.Create;
using Application.Storage.Repository.Contracts.Delete;
using Application.Storage.Repository.Contracts.Read;
using Application.Storage.Repository.Contracts.Update;
using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts;

/// <summary>
///     Complete repository interface for entity persistence operations with a custom identifier type.
///     Combines Create, Read, Update, Delete, and Availability operations for entities with any struct-based identifier.
///     This interface follows the Repository pattern, providing a collection-like interface for domain entities.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct, e.g., int, long, Guid).</typeparam>
public interface IEntityRepository<TEntity, TId> : IReadRepository<TEntity, TId>
    , ICreateRepository<TEntity, TId>
    , IDeleteRepository<TEntity, TId>
    , IUpdateRepository<TEntity, TId>
    , IAvailabilityRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
}