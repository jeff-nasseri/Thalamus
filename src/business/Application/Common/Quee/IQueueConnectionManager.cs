namespace Application.Common.Quee;

/// <summary>
/// Interface for managing queue connections with health monitoring and reconnection capabilities.
/// </summary>
public interface IQueueConnectionManager
{
    /// <summary>
    /// Ensures that a connection to the queue is established.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if connected; otherwise, false.</returns>
    Task<bool> EnsureConnectedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to reconnect to the queue server.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous reconnection operation.</returns>
    Task ReconnectAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current connection state.
    /// </summary>
    /// <returns>The current connection state.</returns>
    QueueConnectionState GetConnectionState();

    /// <summary>
    /// Occurs when the connection state changes.
    /// </summary>
    event Func<QueueConnectionStateChangedEventArgs, Task> ConnectionStateChangedAsync;
}
