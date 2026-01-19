using System.Linq.Expressions;
using Domain.Common.BaseTypes;

#pragma warning disable CS8625

namespace Application.Storage.Repository.Contracts.Read;

/// <summary>
///     Repository interface for reading entities with Guid as the default identifier type.
///     Provides comprehensive query methods for retrieving entities from the data store.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface IReadRepository<TEntity> : IReadRepository<TEntity, Guid>
    where TEntity : BaseEntity<Guid>
{
}

/// <summary>
///     Repository interface for reading entities with a custom identifier type.
///     Provides extensive query capabilities including:
///     - Single entity retrieval (First, Get)
///     - Multiple entity retrieval (GetAll, Where, GetByIds)
///     - Navigation property loading (eager loading)
///     - Change tracking control
///     - Pagination support
///     - Counting operations
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public interface IReadRepository<TEntity, in TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    /// <summary>
    ///     Health check for database connectivity.
    ///     Tests if the repository can successfully connect to the underlying data store.
    /// </summary>
    /// <returns>True if connection is successful; otherwise, false.</returns>
    bool TryConnect();

    /// <summary>
    ///     Retrieves the first entity from the data store, optionally including related entities.
    /// </summary>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">If true, throws an exception when no entity exists; otherwise, returns null.</param>
    /// <returns>The first entity, or null if none exists and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> First(string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves the first entity from the data store.
    /// </summary>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">If true, throws an exception when no entity exists; otherwise, returns null.</param>
    /// <returns>The first entity, or null if none exists and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> First(bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves the first entity from the provided queryable.
    /// </summary>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">If true, throws an exception when no entity exists; otherwise, returns null.</param>
    /// <returns>The first entity from the queryable, or null if none exists and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> First(IQueryable<TEntity> entities
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves the first entity matching the search condition, including related entities.
    /// </summary>
    /// <param name="search">The expression defining the search condition.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no matching entity exists; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The first matching entity, or null if none exists and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> First(Expression<Func<TEntity, bool>> search
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves the first entity matching the search condition.
    /// </summary>
    /// <param name="search">The expression defining the search condition.</param>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no matching entity exists; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The first matching entity, or null if none exists and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> First(Expression<Func<TEntity, bool>> search
        , bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves the first entity matching the search condition from the provided queryable.
    /// </summary>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="search">The expression defining the search condition.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no matching entity exists; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The first matching entity from the queryable, or null if none exists and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> First(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> search
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves an entity by its identifier, including related entities.
    /// </summary>
    /// <param name="key">The entity's identifier.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The entity with the specified identifier, or null if not found and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> Get(TId key
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="key">The entity's identifier.</param>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The entity with the specified identifier, or null if not found and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> Get(TId key
        , bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves an entity by its identifier from the provided queryable.
    /// </summary>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="key">The entity's identifier.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when the entity doesn't exist; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>
    ///     The entity with the specified identifier from the queryable, or null if not found and exceptionRaiseIfNotExist
    ///     is false.
    /// </returns>
    Task<TEntity> Get(IQueryable<TEntity> entities
        , TId key
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves a single entity matching the predicate, including related entities.
    /// </summary>
    /// <param name="predicate">The expression defining the condition to match.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no matching entity exists; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The matching entity, or null if not found and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves a single entity matching the predicate.
    /// </summary>
    /// <param name="predicate">The expression defining the condition to match.</param>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no matching entity exists; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The matching entity, or null if not found and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
        , bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves a single entity matching the predicate from the provided queryable.
    /// </summary>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="predicate">The expression defining the condition to match.</param>
    /// <param name="track">If true, the entity will be tracked by the change tracker; otherwise, it's read-only.</param>
    /// <param name="exceptionRaiseIfNotExist">
    ///     If true, throws an exception when no matching entity exists; otherwise, returns
    ///     null.
    /// </param>
    /// <returns>The matching entity from the queryable, or null if not found and exceptionRaiseIfNotExist is false.</returns>
    Task<TEntity> Get(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> predicate
        , bool track = true
        , bool exceptionRaiseIfNotExist = false);

    /// <summary>
    ///     Retrieves all entities from the data store, including related entities, with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of all entities, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> GetAll<TKey>(
        string navigationPropertyPath
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves all entities from the data store with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of all entities, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> GetAll<TKey>(
        bool includeAllPath = false
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves all entities from the data store with optional pagination.
    /// </summary>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of all entities, optionally paginated.</returns>
    Task<IEnumerable<TEntity>> GetAll(
        bool includeAllPath = false
        , int? page = null
        , bool track = false);

    /// <summary>
    ///     Retrieves all entities from the provided queryable with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of entities from the queryable, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves entities by their identifiers, including related entities, with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="ids">The collection of entity identifiers to retrieve.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of entities with the specified identifiers, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves entities by their identifiers with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="ids">The collection of entity identifiers to retrieve.</param>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of entities with the specified identifiers, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
        , bool includeAllPath
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves entities by their identifiers from the provided queryable with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="ids">The collection of entity identifiers to retrieve.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of entities with the specified identifiers from the queryable, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities
        , IEnumerable<TId> ids
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves entities matching the search condition, including related entities, with optional ordering and
    ///     pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="search">The expression defining the search condition.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of matching entities, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
        , string navigationPropertyPath
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves entities matching the search condition with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="search">The expression defining the search condition.</param>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of matching entities, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
        , bool includeAllPath
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Retrieves entities matching the search condition from the provided queryable with optional ordering and pagination.
    /// </summary>
    /// <typeparam name="TKey">The type of the property to order by.</typeparam>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="search">The expression defining the search condition.</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="orderBy">The function defining the property to order by (null for no ordering).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of matching entities from the queryable, optionally ordered and paginated.</returns>
    Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> search
        , int? page = null
        , Func<TEntity, TKey> orderBy = null
        , bool track = false);

    /// <summary>
    ///     Counts entities matching the condition, including related entities via navigation property.
    /// </summary>
    /// <param name="where">The expression defining the condition to count.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <returns>The number of entities matching the condition.</returns>
    Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath);

    /// <summary>
    ///     Counts entities matching the condition.
    /// </summary>
    /// <param name="where">The expression defining the condition to count.</param>
    /// <returns>The number of entities matching the condition.</returns>
    Task<int> Count(Expression<Func<TEntity, bool>> where);

    /// <summary>
    ///     Counts entities matching the condition in the provided queryable.
    /// </summary>
    /// <param name="entities">The queryable collection of entities.</param>
    /// <param name="where">The expression defining the condition to count.</param>
    /// <returns>The number of entities matching the condition in the queryable.</returns>
    Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where);

    /// <summary>
    ///     Counts all entities in the data store.
    /// </summary>
    /// <returns>The total number of entities.</returns>
    Task<int> Count();
}