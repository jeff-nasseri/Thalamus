using System.Linq.Expressions;
using Domain.Common.BaseTypes;

namespace Application.Storage.Repository.Contracts.Availability;

/// <summary>
///     Repository interface for checking entity availability with Guid as the default identifier type.
///     Provides methods to verify entity existence and validate availability conditions.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public interface IAvailabilityRepository<TEntity> : IAvailabilityRepository<TEntity, Guid>
    where TEntity : BaseEntity<Guid>
{
}

/// <summary>
///     Repository interface for checking entity availability with a custom identifier type.
///     Supports both existence checks (returning boolean) and availability validation (throwing exceptions).
///     Useful for uniqueness validation and constraint checking before operations.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public interface IAvailabilityRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    /// <summary>
    ///     Checks if any entity matching the condition exists, including related entities via navigation property.
    /// </summary>
    /// <param name="any">The expression defining the condition to check.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <returns>True if at least one matching entity exists; otherwise, false.</returns>
    Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath);

    /// <summary>
    ///     Checks if any entity matching the condition exists.
    /// </summary>
    /// <param name="any">The expression defining the condition to check.</param>
    /// <returns>True if at least one matching entity exists; otherwise, false.</returns>
    Task<bool> Any(Expression<Func<TEntity, bool>> any);

    /// <summary>
    ///     Checks if any entity in the provided queryable matches the condition.
    /// </summary>
    /// <param name="entities">The queryable collection of entities to check.</param>
    /// <param name="any">The expression defining the condition to check.</param>
    /// <returns>True if at least one matching entity exists in the queryable; otherwise, false.</returns>
    Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any);

    /// <summary>
    ///     Validates that entities matching the condition exist, including related entities via navigation property.
    ///     Throws an exception if no matching entities are found.
    /// </summary>
    /// <param name="any">The expression defining the condition to validate.</param>
    /// <param name="navigationPropertyPath">The navigation property path to include (e.g., "Author.Books").</param>
    /// <returns>A task representing the asynchronous availability check.</returns>
    /// <exception cref="Exception">Thrown when no matching entities exist.</exception>
    Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath);

    /// <summary>
    ///     Validates that entities matching the condition exist.
    ///     Throws an exception if no matching entities are found.
    /// </summary>
    /// <param name="any">The expression defining the condition to validate.</param>
    /// <returns>A task representing the asynchronous availability check.</returns>
    /// <exception cref="Exception">Thrown when no matching entities exist.</exception>
    Task CheckAvailability(Expression<Func<TEntity, bool>> any);

    /// <summary>
    ///     Validates that entities matching the condition exist in the provided queryable.
    ///     Throws an exception if no matching entities are found.
    /// </summary>
    /// <param name="entities">The queryable collection of entities to check.</param>
    /// <param name="any">The expression defining the condition to validate.</param>
    /// <returns>A task representing the asynchronous availability check.</returns>
    /// <exception cref="Exception">Thrown when no matching entities exist in the queryable.</exception>
    Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any);
}