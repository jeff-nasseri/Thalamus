using System.Linq.Expressions;
using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts.Create;

/// <summary>
///     Repository interface for creating entities with Guid as the default identifier type.
///     Provides methods for inserting single or multiple entities into the data store.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface ICreateRepository<TEntity> : ICreateRepository<TEntity, Guid>
    where TEntity : BaseEntity<Guid>
{
}

/// <summary>
///     Repository interface for creating entities with a custom identifier type.
///     Provides comprehensive methods for inserting entities, including bulk operations and conditional replacements.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public interface ICreateRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    /// <summary>
    ///     Inserts a single entity into the data store.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>A task representing the asynchronous insert operation.</returns>
    Task Insert(TEntity entity);

    /// <summary>
    ///     Inserts multiple entities into the data store in a single operation.
    /// </summary>
    /// <param name="entities">The collection of entities to insert.</param>
    /// <returns>A task representing the asynchronous bulk insert operation.</returns>
    Task InsertRange(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Clears all existing entities of this type from the data store, then inserts the provided entities.
    ///     This is a destructive operation that replaces all existing data.
    /// </summary>
    /// <param name="insertEntities">The collection of entities to insert after clearing.</param>
    /// <returns>A task representing the asynchronous clear and insert operation.</returns>
    Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities);

    /// <summary>
    ///     Removes specific entities from the data store, then inserts new entities.
    ///     Useful for replacing a subset of entities while preserving others.
    /// </summary>
    /// <param name="removeList">The collection of entities to remove.</param>
    /// <param name="insertEntities">The collection of entities to insert after removal.</param>
    /// <returns>A task representing the asynchronous remove and insert operation.</returns>
    Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities);

    /// <summary>
    ///     Deletes entities matching the specified condition, then inserts new entities.
    ///     This operation is transactional - if the insert fails, the delete is rolled back.
    /// </summary>
    /// <param name="deleteCondition">The expression defining which entities to delete.</param>
    /// <param name="insertEntities">The collection of entities to insert after deletion.</param>
    /// <returns>A task representing the asynchronous recreate operation.</returns>
    Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities);
}