using System.Linq.Expressions;
using Application.Storage.Repository.Contracts.Read;
using Domain.Common.BaseTypes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb.Contracts.Read;

/// <summary>
///     MongoDB implementation of the read repository with Guid identifiers.
///     Provides comprehensive query methods for retrieving entities from MongoDB.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbReadRepository<TEntity> : MongoDbReadRepository<TEntity, Guid>, IReadRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbReadRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }
}

/// <summary>
///     MongoDB implementation of the read repository with custom identifier type.
///     Provides extensive query capabilities including filtering, pagination, and counting.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbReadRepository<TEntity, TId> : IReadRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoDatabase Database;
    private const int PageSize = 20;

    public MongoDbReadRepository(IMongoDatabase database, string collectionName)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public bool TryConnect()
    {
        try
        {
            Database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<TEntity> First(string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var entity = await Collection.Find(FilterDefinition<TEntity>.Empty).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} found.");

        return entity;
    }

    public Task<TEntity> First(bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        return First(string.Empty, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> First(IQueryable<TEntity> entities, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var filter = ConvertQueryableToFilter(entities);
        var entity = await Collection.Find(filter).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} found in the queryable.");

        return entity;
    }

    public async Task<TEntity> First(Expression<Func<TEntity, bool>> search, string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var entity = await Collection.Find(search).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the condition found.");

        return entity;
    }

    public Task<TEntity> First(Expression<Func<TEntity, bool>> search, bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        return First(search, string.Empty, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> First(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> search, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var baseFilter = ConvertQueryableToFilter(entities);
        var searchFilter = Builders<TEntity>.Filter.Where(search);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, searchFilter);

        var entity = await Collection.Find(combinedFilter).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the condition found in the queryable.");

        return entity;
    }

    public async Task<TEntity> Get(TId key, string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, key);
        var entity = await Collection.Find(filter).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id {key} not found.");

        return entity;
    }

    public Task<TEntity> Get(TId key, bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        return Get(key, string.Empty, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get(IQueryable<TEntity> entities, TId key, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var baseFilter = ConvertQueryableToFilter(entities);
        var keyFilter = Builders<TEntity>.Filter.Eq(e => e.Id, key);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, keyFilter);

        var entity = await Collection.Find(combinedFilter).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with Id {key} not found in the queryable.");

        return entity;
    }

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var entity = await Collection.Find(predicate).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the predicate found.");

        return entity;
    }

    public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        return Get(predicate, string.Empty, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> predicate, bool track = true, bool exceptionRaiseIfNotExist = false)
    {
        var baseFilter = ConvertQueryableToFilter(entities);
        var predicateFilter = Builders<TEntity>.Filter.Where(predicate);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, predicateFilter);

        var entity = await Collection.Find(combinedFilter).FirstOrDefaultAsync();

        if (entity == null && exceptionRaiseIfNotExist)
            throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} matching the predicate found in the queryable.");

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAll<TKey>(string navigationPropertyPath, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        var query = Collection.Find(FilterDefinition<TEntity>.Empty);

        if (page.HasValue)
            query = query.Skip((page.Value - 1) * PageSize).Limit(PageSize);

        var results = await query.ToListAsync();

        if (orderBy != null)
            results = results.OrderBy(orderBy).ToList();

        return results;
    }

    public Task<IEnumerable<TEntity>> GetAll<TKey>(bool includeAllPath = false, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        return GetAll(string.Empty, page, orderBy, track);
    }

    public Task<IEnumerable<TEntity>> GetAll(bool includeAllPath = false, int? page = null, bool track = false)
    {
        return GetAll<object>(includeAllPath, page, null, track);
    }

    public async Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        var filter = ConvertQueryableToFilter(entities);
        var query = Collection.Find(filter);

        if (page.HasValue)
            query = query.Skip((page.Value - 1) * PageSize).Limit(PageSize);

        var results = await query.ToListAsync();

        if (orderBy != null)
            results = results.OrderBy(orderBy).ToList();

        return results;
    }

    public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids, string navigationPropertyPath, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        var filter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        var query = Collection.Find(filter);

        if (page.HasValue)
            query = query.Skip((page.Value - 1) * PageSize).Limit(PageSize);

        var results = await query.ToListAsync();

        if (orderBy != null)
            results = results.OrderBy(orderBy).ToList();

        return results;
    }

    public Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids, bool includeAllPath, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        return GetByIds(ids, string.Empty, page, orderBy, track);
    }

    public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities, IEnumerable<TId> ids, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        var baseFilter = ConvertQueryableToFilter(entities);
        var idsFilter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, idsFilter);

        var query = Collection.Find(combinedFilter);

        if (page.HasValue)
            query = query.Skip((page.Value - 1) * PageSize).Limit(PageSize);

        var results = await query.ToListAsync();

        if (orderBy != null)
            results = results.OrderBy(orderBy).ToList();

        return results;
    }

    public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search, string navigationPropertyPath, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        var query = Collection.Find(search);

        if (page.HasValue)
            query = query.Skip((page.Value - 1) * PageSize).Limit(PageSize);

        var results = await query.ToListAsync();

        if (orderBy != null)
            results = results.OrderBy(orderBy).ToList();

        return results;
    }

    public Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search, bool includeAllPath, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        return Where(search, string.Empty, page, orderBy, track);
    }

    public async Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> search, int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
    {
        var baseFilter = ConvertQueryableToFilter(entities);
        var searchFilter = Builders<TEntity>.Filter.Where(search);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, searchFilter);

        var query = Collection.Find(combinedFilter);

        if (page.HasValue)
            query = query.Skip((page.Value - 1) * PageSize).Limit(PageSize);

        var results = await query.ToListAsync();

        if (orderBy != null)
            results = results.OrderBy(orderBy).ToList();

        return results;
    }

    public Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
    {
        return Count(where);
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> where)
    {
        return (int)await Collection.CountDocumentsAsync(where);
    }

    public async Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
    {
        var baseFilter = ConvertQueryableToFilter(entities);
        var whereFilter = Builders<TEntity>.Filter.Where(where);
        var combinedFilter = Builders<TEntity>.Filter.And(baseFilter, whereFilter);

        return (int)await Collection.CountDocumentsAsync(combinedFilter);
    }

    public async Task<int> Count()
    {
        return (int)await Collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty);
    }

    protected virtual FilterDefinition<TEntity> ConvertQueryableToFilter(IQueryable<TEntity> entities)
    {
        // Simplified implementation - returns empty filter matching all documents
        return FilterDefinition<TEntity>.Empty;
    }
}
