using Domain.Common.BaseTypes;
using ErrorHandling;

namespace Application.Storage;

/// <summary>
///     Secure repository interface for memory storage access.
///     Provides controlled access to memory data through query and command operations.
///     Extends IMemoryAccess with read and write capabilities for memory aggregate roots.
/// </summary>
public interface IMemorySecureAccessRepository : IMemoryAccess
{
    /// <summary>
    ///     Executes a read query against the memory storage and returns typed results.
    /// </summary>
    /// <typeparam name="TResult">The type of memory aggregate root to return (must implement IMemoryAggregateRoot).</typeparam>
    /// <param name="query">The query string to execute against the memory storage.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response containing the query results of type TResult.</returns>
    Task<Response<TResult>> ExecuteQueryAsync<TResult>(string query, CancellationToken cancellationToken)
        where TResult : IMemoryAggregateRoot;

    /// <summary>
    ///     Executes a write command against the memory storage.
    ///     Used for creating, updating, or deleting memory data.
    /// </summary>
    /// <param name="command">The command string to execute against the memory storage.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating success or failure of the command execution.</returns>
    Task<Response> ExecuteCommandAsync(string command, CancellationToken cancellationToken);
}