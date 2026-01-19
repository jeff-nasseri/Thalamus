using Application.Storage.Repository.Contracts.Graph;
using Domain.Common.BaseTypes;
using MongoDB.Driver;

namespace Infrastructure.Storage.NoSql.MongoDb.Contracts.Graph;

/// <summary>
///     MongoDB implementation of the graph repository with Guid identifiers.
///     Provides dynamic query capabilities using aggregation pipelines.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity with Guid identifier.</typeparam>
public class MongoDbGraphRepository<TEntity> : MongoDbGraphRepository<TEntity, Guid>, IGraphRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    public MongoDbGraphRepository(IMongoDatabase database, string collectionName)
        : base(database, collectionName)
    {
    }
}

/// <summary>
///     MongoDB implementation of the graph repository with custom identifier type.
///     Note: String-based dynamic queries are not directly supported in MongoDB.
///     Consider using MongoDB aggregation pipelines or LINQ expressions instead.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (must be a struct).</typeparam>
public class MongoDbGraphRepository<TEntity, TId> : IGraphRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly IMongoDatabase Database;

    public MongoDbGraphRepository(IMongoDatabase database, string collectionName)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        Collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
    }

    public Task<IEnumerable<dynamic>> GraphQuery(string navigationPropertyPath, string where, string select, string orderBy, int? page = null, bool track = false)
    {
        throw new NotSupportedException(
            "Graph queries with string-based expressions are not directly supported in MongoDB. " +
            "Consider using MongoDB aggregation pipelines with BsonDocument or implementing a custom query builder. " +
            "For dynamic queries, you can use the MongoDB.Driver's PipelineDefinition or typed aggregation operations.");
    }

    public Task<IEnumerable<dynamic>> GraphQuery(bool includeAllPath, string where, string select, string orderBy, int? page = null, bool track = false)
    {
        throw new NotSupportedException(
            "Graph queries with string-based expressions are not directly supported in MongoDB. " +
            "Consider using MongoDB aggregation pipelines with BsonDocument or implementing a custom query builder. " +
            "For dynamic queries, you can use the MongoDB.Driver's PipelineDefinition or typed aggregation operations.");
    }

    public Task<IEnumerable<dynamic>> GraphQuery(IQueryable<TEntity> entities, string where, string select, string orderBy, int? page = null, bool track = false)
    {
        throw new NotSupportedException(
            "Graph queries with string-based expressions are not directly supported in MongoDB. " +
            "Consider using MongoDB aggregation pipelines with BsonDocument or implementing a custom query builder. " +
            "For dynamic queries, you can use the MongoDB.Driver's PipelineDefinition or typed aggregation operations.");
    }
}
