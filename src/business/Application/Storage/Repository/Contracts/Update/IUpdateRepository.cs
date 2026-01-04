using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts.Update;

/// <summary>
///     Repository interface for updating entities with Guid as the default identifier type.
///     Provides methods for modifying existing entities in the data store.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface IUpdateRepository<TEntity> : IUpdateRepository<TEntity, Guid>
    where TEntity : BaseEntity<Guid>
{
}

/// <summary>
///     Repository interface for updating entities with a custom identifier type.
///     Handles modification of existing entity data in the persistence layer.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public interface IUpdateRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    /// <summary>
    ///     Updates an existing entity in the data store.
    ///     The entity is identified by its Id property and all properties are updated.
    /// </summary>
    /// <param name="entity">The entity with updated values to persist.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, silently
    ///     succeeds.
    /// </param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false);
}