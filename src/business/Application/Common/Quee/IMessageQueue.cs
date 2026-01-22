namespace Application.Common.Quee;

/// <summary>
/// Core interface for message queue operations providing connection management,
/// message publishing/consuming, and event handling.
/// </summary>
public interface IMessageQueue
{
    /// <summary>
    /// Establishes a connection to the message queue server.
    /// </summary>
    /// <param name="options">Configuration options for the connection.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous connection operation.</returns>
    Task ConnectAsync(
        QueueConnectionOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Disconnects from the message queue server.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous disconnection operation.</returns>
    Task DisconnectAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Enqueues a message to the specified queue.
    /// </summary>
    /// <param name="message">The message to enqueue.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the enqueue operation.</returns>
    Task<EnqueueResult> EnqueueAsync(
        QueueMessage message,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Dequeues a message from the specified queue.
    /// </summary>
    /// <param name="options">Subscription options for dequeuing.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the dequeue operation.</returns>
    Task<DequeueResult> DequeueAsync(
        QueueSubscriptionOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unsubscribes from the specified queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to unsubscribe from.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the unsubscribe operation.</returns>
    Task<UnsubscribeResult> UnsubscribeAsync(
        string queueName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a value indicating whether the queue is currently connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Occurs when a message is received from the queue.
    /// </summary>
    event Func<QueueMessageReceivedEventArgs, Task> MessageReceivedAsync;

    /// <summary>
    /// Occurs when the connection to the queue is established.
    /// </summary>
    event Func<QueueConnectedEventArgs, Task> ConnectedAsync;

    /// <summary>
    /// Occurs when the connection to the queue is terminated.
    /// </summary>
    event Func<QueueDisconnectedEventArgs, Task> DisconnectedAsync;
}
