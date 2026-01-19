using System.Linq.Expressions;
using Application.Storage.Repository.Contracts.Delete;
using Domain.Common.BaseTypes;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb.Contracts.Delete;

/// <summary>
///     MongoDB implementation of the delete repository with Guid identifiers.
///     Provides methods for hard deletion and soft deletion of entities.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbDeleteRepository<TEntity> : MongoDbDeleteRepository<TEntity, Guid>, IDeleteRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbDeleteRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }
}

/// <summary>
///     MongoDB implementation of the delete repository with custom identifier type.
///     Supports both hard deletion (permanent removal) and soft deletion (marking as deleted).
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbDeleteRepository<TEntity, TId> : IDeleteRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoDatabase Database;

    public MongoDbDeleteRepository(IMongoDatabase database, string collectionName)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public async Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await Delete(entity.Id, exceptionRaiseIfNotExist);
    }

    public async Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        var result = await Collection.DeleteOneAsync(filter);

        if (result.DeletedCount == 0 && exceptionRaiseIfNotExist)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id {id} not found for deletion.");
    }

    public async Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
    {
        if (single == null)
            throw new ArgumentNullException(nameof(single));

        var result = await Collection.DeleteOneAsync(single);

        if (result.DeletedCount == 0 && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the condition found for deletion.");
    }

    public async Task DeleteRange(IEnumerable<TEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        var ids = entities.Select(e => e.Id).ToList();
        if (!ids.Any())
            return;

        var filter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        await Collection.DeleteManyAsync(filter);
    }

    public async Task DeleteRange(Expression<Func<TEntity, bool>> where)
    {
        if (where == null)
            throw new ArgumentNullException(nameof(where));

        await Collection.DeleteManyAsync(where);
    }

    public async Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        var update = Builders<TEntity>.Update.Set(e => e.DeletedDateTime, DateTime.UtcNow);

        var result = await Collection.UpdateOneAsync(filter, update);

        if (result.MatchedCount == 0 && exceptionRaiseIfNotExist)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id {id} not found for soft deletion.");
    }
}
