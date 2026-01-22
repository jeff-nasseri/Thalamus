namespace Application.Common.Quee;

/// <summary>
/// Interface for persistent queue operations supporting offline message queuing
/// and recovery scenarios.
/// </summary>
public interface IPersistentQueue
{
    /// <summary>
    /// Enqueues a message to persistent storage.
    /// </summary>
    /// <param name="message">The message to enqueue.</param>
    /// <returns>A task representing the asynchronous enqueue operation.</returns>
    Task EnqueueAsync(QueueMessage message);

    /// <summary>
    /// Dequeues the next message from persistent storage.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The dequeued message, or null if the queue is empty.</returns>
    Task<QueueMessage> DequeueAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Peeks at the next message without removing it from the queue.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The next message, or null if the queue is empty.</returns>
    Task<QueueMessage> PeekAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Dequeues all messages from persistent storage.
    /// </summary>
    /// <returns>A collection of all messages in the queue.</returns>
    Task<IEnumerable<QueueMessage>> DequeueAllAsync();

    /// <summary>
    /// Gets the current count of messages in the persistent queue.
    /// </summary>
    /// <returns>The number of messages in the queue.</returns>
    Task<int> GetCountAsync();

    /// <summary>
    /// Clears all messages from the persistent queue.
    /// </summary>
    /// <returns>A task representing the asynchronous clear operation.</returns>
    Task ClearAsync();

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    bool IsEmpty { get; }
}
