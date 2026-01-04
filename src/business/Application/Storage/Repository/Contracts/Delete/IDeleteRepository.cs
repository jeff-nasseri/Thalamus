using System.Linq.Expressions;
using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts.Delete;

/// <summary>
///     Repository interface for deleting entities with Guid as the default identifier type.
///     Provides methods for hard deletion and soft deletion of entities.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface IDeleteRepository<TEntity> : IDeleteRepository<TEntity, Guid>
    where TEntity : BaseEntity<Guid>
{
}

/// <summary>
///     Repository interface for deleting entities with a custom identifier type.
///     Supports both hard deletion (permanent removal) and soft deletion (marking as deleted).
///     Provides flexible deletion methods including single entity, multiple entities, and conditional deletion.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public interface IDeleteRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    /// <summary>
    ///     Permanently deletes the specified entity from the data store.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, silently
    ///     succeeds.
    /// </param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Permanently deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, silently
    ///     succeeds.
    /// </param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    Task Delete(TId id, bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Permanently deletes a single entity matching the specified condition.
    /// </summary>
    /// <param name="single">The expression defining which entity to delete.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no entity matches; otherwise, silently
    ///     succeeds.
    /// </param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Permanently deletes multiple entities from the data store.
    /// </summary>
    /// <param name="entities">The collection of entities to delete.</param>
    /// <returns>A task representing the asynchronous bulk delete operation.</returns>
    Task DeleteRange(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Permanently deletes all entities matching the specified condition.
    /// </summary>
    /// <param name="where">The expression defining which entities to delete.</param>
    /// <returns>A task representing the asynchronous conditional delete operation.</returns>
    Task DeleteRange(Expression<Func<TEntity, bool>> where);

    /// <summary>
    ///     Performs a soft delete on an entity by marking it as deleted without removing it from the data store.
    ///     The entity remains in the database but is excluded from normal queries.
    /// </summary>
    /// <param name="id">The identifier of the entity to soft delete.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, silently
    ///     succeeds.
    /// </param>
    /// <returns>A task representing the asynchronous soft delete operation.</returns>
    Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false);
}