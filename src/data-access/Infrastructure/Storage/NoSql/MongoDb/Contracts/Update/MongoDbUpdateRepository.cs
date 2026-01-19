using Application.Storage.Repository.Contracts.Update;
using Domain.Common.BaseTypes;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb.Contracts.Update;

/// <summary>
///     MongoDB implementation of the update repository with Guid identifiers.
///     Provides methods for modifying existing entities in MongoDB.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbUpdateRepository<TEntity> : MongoDbUpdateRepository<TEntity, Guid>, IUpdateRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbUpdateRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }
}

/// <summary>
///     MongoDB implementation of the update repository with custom identifier type.
///     Handles modification of existing entity data in MongoDB collections.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbUpdateRepository<TEntity, TId> : IUpdateRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoDatabase Database;

    public MongoDbUpdateRepository(IMongoDatabase database, string collectionName)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public async Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        entity.ModifiedDateTime = DateTime.UtcNow;

        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
        var result = await Collection.ReplaceOneAsync(filter, entity);

        if (result.MatchedCount == 0 && exceptionRaiseIfNotExist)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id {entity.Id} not found for update.");
    }
}
