using System.Linq.Expressions;
using Application.Storage.Repository.Contracts;
using Application.Storage.Repository.Contracts.Availability;
using Application.Storage.Repository.Contracts.Create;
using Application.Storage.Repository.Contracts.Delete;
using Application.Storage.Repository.Contracts.Graph;
using Application.Storage.Repository.Contracts.Read;
using Application.Storage.Repository.Contracts.Update;
using Domain.Common.BaseTypes;
using Infrastructure.Storage.NoSql.MongoDb.Contracts.Availability;
using Infrastructure.Storage.NoSql.MongoDb.Contracts.Create;
using Infrastructure.Storage.NoSql.MongoDb.Contracts.Delete;
using Infrastructure.Storage.NoSql.MongoDb.Contracts.Graph;
using Infrastructure.Storage.NoSql.MongoDb.Contracts.Read;
using Infrastructure.Storage.NoSql.MongoDb.Contracts.Update;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb;

/// <summary>
///     MongoDB implementation of the complete entity repository with Guid identifiers.
///     Provides full CRUD operations and query capabilities for entities stored in MongoDB.
///     Uses dependency injection to delegate operations to specialized repository implementations.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbEntityRepository<TEntity> : MongoDbEntityRepository<TEntity, Guid>, IEntityRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbEntityRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }

    public MongoDbEntityRepository(
        IReadRepository<TEntity, Guid> readRepository,
        ICreateRepository<TEntity, Guid> createRepository,
        IUpdateRepository<TEntity, Guid> updateRepository,
        IDeleteRepository<TEntity, Guid> deleteRepository,
        IAvailabilityRepository<TEntity, Guid> availabilityRepository,
        IGraphRepository<TEntity, Guid> graphRepository)
        : base(readRepository, createRepository, updateRepository, deleteRepository, availabilityRepository,
            graphRepository)
    {
    }
}

/// <summary>
///     MongoDB implementation of the complete entity repository with custom identifier type.
///     Combines Create, Read, Update, Delete, and Availability operations for MongoDB collections.
///     Uses composition and dependency injection to avoid code duplication.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbEntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    private readonly IReadRepository<TEntity, TId> _readRepository;
    private readonly ICreateRepository<TEntity, TId> _createRepository;
    private readonly IUpdateRepository<TEntity, TId> _updateRepository;
    private readonly IDeleteRepository<TEntity, TId> _deleteRepository;
    private readonly IAvailabilityRepository<TEntity, TId> _availabilityRepository;
    private readonly IGraphRepository<TEntity, TId> _graphRepository;

    /// <summary>
    ///     Initializes a new instance with direct database access.
    ///     Creates all specialized repositories internally.
    /// </summary>
    /// <param name="database">The MongoDB database instance.</param>
    /// <param name="collectionName">The collection name (defaults to entity type name).</param>
    public MongoDbEntityRepository(IMongoDatabase database, string collectionName)
    {
        if (database == null)
            throw new ArgumentNullException(nameof(database));

        var resolvedCollectionName = collectionName ?? typeof(TEntity).Name;

        _readRepository = new MongoDbReadRepository<TEntity, TId>(database, resolvedCollectionName);
        _createRepository = new MongoDbCreateRepository<TEntity, TId>(database, resolvedCollectionName);
        _updateRepository = new MongoDbUpdateRepository<TEntity, TId>(database, resolvedCollectionName);
        _deleteRepository = new MongoDbDeleteRepository<TEntity, TId>(database, resolvedCollectionName);
        _availabilityRepository = new MongoDbAvailabilityRepository<TEntity, TId>(database, resolvedCollectionName);
        _graphRepository = new MongoDbGraphRepository<TEntity, TId>(database, resolvedCollectionName);
    }

    /// <summary>
    ///     Initializes a new instance with injected specialized repositories.
    ///     Allows for custom repository implementations and testing.
    /// </summary>
    /// <param name="readRepository">The read repository implementation.</param>
    /// <param name="createRepository">The create repository implementation.</param>
    /// <param name="updateRepository">The update repository implementation.</param>
    /// <param name="deleteRepository">The delete repository implementation.</param>
    /// <param name="availabilityRepository">The availability repository implementation.</param>
    /// <param name="graphRepository">The graph repository implementation.</param>
    public MongoDbEntityRepository(
        IReadRepository<TEntity, TId> readRepository,
        ICreateRepository<TEntity, TId> createRepository,
        IUpdateRepository<TEntity, TId> updateRepository,
        IDeleteRepository<TEntity, TId> deleteRepository,
        IAvailabilityRepository<TEntity, TId> availabilityRepository,
        IGraphRepository<TEntity, TId> graphRepository)
    {
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
        _createRepository = createRepository ?? throw new ArgumentNullException(nameof(createRepository));
        _updateRepository = updateRepository ?? throw new ArgumentNullException(nameof(updateRepository));
        _deleteRepository = deleteRepository ?? throw new ArgumentNullException(nameof(deleteRepository));
        _availabilityRepository =
            availabilityRepository ?? throw new ArgumentNullException(nameof(availabilityRepository));
        _graphRepository = graphRepository ?? throw new ArgumentNullException(nameof(graphRepository));
    }

    public bool TryConnect() => _readRepository.TryConnect();

    public Task<TEntity> First(string navigationPropertyPath, bool track = true, bool exceptionRaiseIfNotExist = false)
        => _readRepository.First(navigationPropertyPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> First(bool includeAllPath = false, bool track = true, bool exceptionRaiseIfNotExist = false)
        => _readRepository.First(includeAllPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> First(IQueryable<TEntity> entities, bool track = true, bool exceptionRaiseIfNotExist = false)
        => _readRepository.First(entities, track, exceptionRaiseIfNotExist);

    public Task<TEntity> First(Expression<Func<TEntity, bool>> search, string navigationPropertyPath, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.First(search, navigationPropertyPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> First(Expression<Func<TEntity, bool>> search, bool includeAllPath = false, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.First(search, includeAllPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> First(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> search, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.First(entities, search, track, exceptionRaiseIfNotExist);

    public Task<TEntity> Get(TId key, string navigationPropertyPath, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.Get(key, navigationPropertyPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> Get(TId key, bool includeAllPath = false, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.Get(key, includeAllPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> Get(IQueryable<TEntity> entities, TId key, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.Get(entities, key, track, exceptionRaiseIfNotExist);

    public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, string navigationPropertyPath,
        bool track = true, bool exceptionRaiseIfNotExist = false)
        => _readRepository.Get(predicate, navigationPropertyPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool includeAllPath = false, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.Get(predicate, includeAllPath, track, exceptionRaiseIfNotExist);

    public Task<TEntity> Get(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> predicate, bool track = true,
        bool exceptionRaiseIfNotExist = false)
        => _readRepository.Get(entities, predicate, track, exceptionRaiseIfNotExist);

    public Task<IEnumerable<TEntity>> GetAll<TKey>(string navigationPropertyPath, int? page = null,
        Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.GetAll(navigationPropertyPath, page, orderBy, track);

    public Task<IEnumerable<TEntity>> GetAll<TKey>(bool includeAllPath = false, int? page = null,
        Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.GetAll(includeAllPath, page, orderBy, track);

    public Task<IEnumerable<TEntity>> GetAll(bool includeAllPath = false, int? page = null, bool track = false)
        => _readRepository.GetAll(includeAllPath, page, track);

    public Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities, int? page = null,
        Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.GetAll(entities, page, orderBy, track);

    public Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids, string navigationPropertyPath,
        int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.GetByIds(ids, navigationPropertyPath, page, orderBy, track);

    public Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids, bool includeAllPath, int? page = null,
        Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.GetByIds(ids, includeAllPath, page, orderBy, track);

    public Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities, IEnumerable<TId> ids,
        int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.GetByIds(entities, ids, page, orderBy, track);

    public Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search, string navigationPropertyPath,
        int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.Where(search, navigationPropertyPath, page, orderBy, track);

    public Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search, bool includeAllPath,
        int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.Where(search, includeAllPath, page, orderBy, track);

    public Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> search,
        int? page = null, Func<TEntity, TKey> orderBy = null, bool track = false)
        => _readRepository.Where(entities, search, page, orderBy, track);

    public Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
        => _readRepository.Count(where, navigationPropertyPath);

    public Task<int> Count(Expression<Func<TEntity, bool>> where)
        => _readRepository.Count(where);

    public Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
        => _readRepository.Count(entities, where);

    public Task<int> Count()
        => _readRepository.Count();

    public Task Insert(TEntity entity)
        => _createRepository.Insert(entity);

    public Task InsertRange(IEnumerable<TEntity> entities)
        => _createRepository.InsertRange(entities);

    public Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities)
        => _createRepository.ClearAllEntitiesThenAddRange(insertEntities);

    public Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
        => _createRepository.ClearRemoveListThenAddRange(removeList, insertEntities);

    public Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
        => _createRepository.ReCreate(deleteCondition, insertEntities);

    public Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
        => _updateRepository.Update(entity, exceptionRaiseIfNotExist);

    public Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false)
        => _deleteRepository.Delete(entity, exceptionRaiseIfNotExist);

    public Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
        => _deleteRepository.Delete(id, exceptionRaiseIfNotExist);

    public Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
        => _deleteRepository.Delete(single, exceptionRaiseIfNotExist);

    public Task DeleteRange(IEnumerable<TEntity> entities)
        => _deleteRepository.DeleteRange(entities);

    public Task DeleteRange(Expression<Func<TEntity, bool>> where)
        => _deleteRepository.DeleteRange(where);

    public Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
        => _deleteRepository.SoftDelete(id, exceptionRaiseIfNotExist);

    public Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
        => _availabilityRepository.Any(any, navigationPropertyPath);

    public Task<bool> Any(Expression<Func<TEntity, bool>> any)
        => _availabilityRepository.Any(any);

    public Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
        => _availabilityRepository.Any(entities, any);

    public Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
        => _availabilityRepository.CheckAvailability(any, navigationPropertyPath);

    public Task CheckAvailability(Expression<Func<TEntity, bool>> any)
        => _availabilityRepository.CheckAvailability(any);

    public Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
        => _availabilityRepository.CheckAvailability(entities, any);

    public Task<IEnumerable<dynamic>> GraphQuery(string navigationPropertyPath, string where, string select,
        string orderBy, int? page = null, bool track = false)
        => _graphRepository.GraphQuery(navigationPropertyPath, where, select, orderBy, page, track);

    public Task<IEnumerable<dynamic>> GraphQuery(bool includeAllPath, string where, string select, string orderBy,
        int? page = null, bool track = false)
        => _graphRepository.GraphQuery(includeAllPath, where, select, orderBy, page, track);

    public Task<IEnumerable<dynamic>> GraphQuery(IQueryable<TEntity> entities, string where, string select,
        string orderBy, int? page = null, bool track = false)
        => _graphRepository.GraphQuery(entities, where, select, orderBy, page, track);
}