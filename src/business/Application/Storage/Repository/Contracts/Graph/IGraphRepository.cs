using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts.Graph;

/// <summary>
///     Repository interface for executing graph-based queries with Guid as the default identifier type.
///     Provides dynamic query capabilities using string-based query expressions.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface IGraphRepository<in TEntity> : IGraphRepository<TEntity, Guid>
    where TEntity : BaseEntity<Guid>
{
}

/// <summary>
///     Repository interface for executing graph-based queries with a custom identifier type.
///     Supports dynamic LINQ-style queries using string expressions for where, select, and orderBy clauses.
///     Useful for building flexible query APIs and dynamic reporting features.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public interface IGraphRepository<in TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    /// <summary>
    ///     Executes a dynamic query with string-based expressions, including related entities via navigation property.
    /// </summary>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <param name="where">String expression for filtering (e.g., "Age > 18 AND IsActive == true").</param>
    /// <param name="select">String expression for projection (e.g., "new { Name, Email }").</param>
    /// <param name="orderBy">String expression for ordering (e.g., "Name ASC, CreatedDate DESC").</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of dynamic objects representing the query results.</returns>
    Task<IEnumerable<dynamic>> GraphQuery(string navigationPropertyPath
        , string where
        , string select
        , string orderBy
        , int? page = null
        , bool track = false);

    /// <summary>
    ///     Executes a dynamic query with string-based expressions.
    /// </summary>
    /// <param name="includeAllPath">If true, includes all navigation properties; otherwise, includes none.</param>
    /// <param name="where">String expression for filtering (e.g., "Age > 18 AND IsActive == true").</param>
    /// <param name="select">String expression for projection (e.g., "new { Name, Email }").</param>
    /// <param name="orderBy">String expression for ordering (e.g., "Name ASC, CreatedDate DESC").</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of dynamic objects representing the query results.</returns>
    Task<IEnumerable<dynamic>> GraphQuery(bool includeAllPath
        , string where
        , string select
        , string orderBy
        , int? page = null
        , bool track = false);

    /// <summary>
    ///     Executes a dynamic query with string-based expressions on the provided queryable.
    /// </summary>
    /// <param name="entities">The queryable collection of entities to query.</param>
    /// <param name="where">String expression for filtering (e.g., "Age > 18 AND IsActive == true").</param>
    /// <param name="select">String expression for projection (e.g., "new { Name, Email }").</param>
    /// <param name="orderBy">String expression for ordering (e.g., "Name ASC, CreatedDate DESC").</param>
    /// <param name="page">The page number for pagination (null for no pagination).</param>
    /// <param name="track">If true, entities will be tracked by the change tracker; otherwise, they're read-only.</param>
    /// <returns>A collection of dynamic objects representing the query results.</returns>
    Task<IEnumerable<dynamic>> GraphQuery(IQueryable<TEntity> entities
        , string where
        , string select
        , string orderBy
        , int? page = null
        , bool track = false);
}