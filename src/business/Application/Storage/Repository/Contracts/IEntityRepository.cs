using Application.Storage.Repository.Contracts.Availability;
using Application.Storage.Repository.Contracts.Create;
using Application.Storage.Repository.Contracts.Delete;
using Application.Storage.Repository.Contracts.Update;
using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts;

/// <summary>
///     Complete repository interface for entity persistence operations with Guid as the default identifier type.
///     Combines Create, Read, Update, Delete, and Availability operations for entities with Guid identifiers.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    , ICreateRepository<TEntity>
    , IDeleteRepository<TEntity>
    , IUpdateRepository<TEntity>
    , IAvailabilityRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
}