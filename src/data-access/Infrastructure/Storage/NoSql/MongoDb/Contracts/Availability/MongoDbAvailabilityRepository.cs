using System.Linq.Expressions;
using Application.Storage.Repository.Contracts.Availability;
using Domain.Common.BaseTypes;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb.Contracts.Availability;

/// <summary>
///     MongoDB implementation of the availability repository with Guid identifiers.
///     Provides methods to verify entity existence and validate availability conditions.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbAvailabilityRepository<TEntity> : MongoDbAvailabilityRepository<TEntity, Guid>, IAvailabilityRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbAvailabilityRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }
}

/// <summary>
///     MongoDB implementation of the availability repository with custom identifier type.
///     Supports both existence checks (returning boolean) and availability validation (throwing exceptions).
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbAvailabilityRepository<TEntity, TId> : IAvailabilityRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoDatabase Database;

    public MongoDbAvailabilityRepository(IMongoDatabase database, string collectionName)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
    {
        return Any(any);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> any)
    {
        if (any == null)
            throw new ArgumentNullException(nameof(any));

        var count = await Collection.CountDocumentsAsync(any, new CountOptions { Limit = 1 });
        return count > 0;
    }

    public async Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        if (any == null)
            throw new ArgumentNullException(nameof(any));

        var baseFilter = ConvertQueryableToFilter(entities);
        var anyFilter = Builders<TEntity>.Filter.Where(any);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, anyFilter);

        var count = await Collection.CountDocumentsAsync(combinedFilter, new CountOptions { Limit = 1 });
        return count > 0;
    }

    public async Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
    {
        await CheckAvailability(any);
    }

    public async Task CheckAvailability(Expression<Func<TEntity, bool>> any)
    {
        var exists = await Any(any);
        if (!exists)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the condition is available.");
    }

    public async Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
    {
        var exists = await Any(entities, any);
        if (!exists)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the condition is available in the queryable.");
    }

    protected virtual FilterDefinition<TEntity> ConvertQueryableToFilter(IQueryable<TEntity> entities)
    {
        // Simplified implementation - returns empty filter matching all documents
        return FilterDefinition<TEntity>.Empty;
    }
}
