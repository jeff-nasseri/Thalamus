namespace Application.Services.StorageHealthCheck;

/// <summary>
/// Provides health check services for storage connectivity.
/// </summary>
public interface IStorageHealthCheckService
{
    /// <summary>
    /// Attempts to connect to the storage synchronously.
    /// </summary>
    /// <returns>True if the connection succeeds; otherwise, false.</returns>
    bool TryConnect();

    /// <summary>
    /// Attempts to connect to the storage asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the async operation, with true if connection succeeds; otherwise, false.</returns>
    Task<bool> TryConnectAsync(CancellationToken cancellationToken);
}