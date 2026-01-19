using System.Linq.Expressions;
using Application.Storage.Repository.Contracts.Create;
using Domain.Common.BaseTypes;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb.Contracts.Create;

/// <summary>
///     MongoDB implementation of the create repository with Guid identifiers.
///     Provides methods for inserting single or multiple entities into MongoDB.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbCreateRepository<TEntity> : MongoDbCreateRepository<TEntity, Guid>, ICreateRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbCreateRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }
}

/// <summary>
///     MongoDB implementation of the create repository with custom identifier type.
///     Handles comprehensive insert operations including bulk operations and conditional replacements.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbCreateRepository<TEntity, TId> : ICreateRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoDatabase Database;

    public MongoDbCreateRepository(IMongoDatabase database, string collectionName)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public async Task Insert(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        entity.CreatedDateTime = DateTime.UtcNow;
        await Collection.InsertOneAsync(entity);
    }

    public async Task InsertRange(IEnumerable<TEntity> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        var entityList = entities.ToList();
        if (!entityList.Any())
            return;

        foreach (var entity in entityList)
        {
            entity.CreatedDateTime = DateTime.UtcNow;
        }

        await Collection.InsertManyAsync(entityList);
    }

    public async Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities)
    {
        if (insertEntities == null)
            throw new ArgumentNullException(nameof(insertEntities));

        using var session = await Database.Client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            await Collection.DeleteManyAsync(session, FilterDefinition<TEntity>.Empty);

            var entityList = insertEntities.ToList();
            if (entityList.Any())
            {
                foreach (var entity in entityList)
                {
                    entity.CreatedDateTime = DateTime.UtcNow;
                }

                await Collection.InsertManyAsync(session, entityList);
            }

            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
    {
        if (removeList == null)
            throw new ArgumentNullException(nameof(removeList));
        if (insertEntities == null)
            throw new ArgumentNullException(nameof(insertEntities));

        using var session = await Database.Client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            var removeIds = removeList.Select(e => e.Id).ToList();
            if (removeIds.Any())
            {
                var filter = Builders<TEntity>.Filter.In(e => e.Id, removeIds);
                await Collection.DeleteManyAsync(session, filter);
            }

            var entityList = insertEntities.ToList();
            if (entityList.Any())
            {
                foreach (var entity in entityList)
                {
                    entity.CreatedDateTime = DateTime.UtcNow;
                }

                await Collection.InsertManyAsync(session, entityList);
            }

            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
    {
        if (deleteCondition == null)
            throw new ArgumentNullException(nameof(deleteCondition));
        if (insertEntities == null)
            throw new ArgumentNullException(nameof(insertEntities));

        using var session = await Database.Client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            await Collection.DeleteManyAsync(session, deleteCondition);

            var entityList = insertEntities.ToList();
            if (entityList.Any())
            {
                foreach (var entity in entityList)
                {
                    entity.CreatedDateTime = DateTime.UtcNow;
                }

                await Collection.InsertManyAsync(session, entityList);
            }

            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}
