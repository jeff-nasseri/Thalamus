namespace Application.Common.Quee;

/// <summary>
/// Interface for managing failed messages that could not be processed successfully.
/// </summary>
public interface IDeadLetterQueue
{
    /// <summary>
    /// Enqueues a failed message to the dead letter queue with a failure reason.
    /// </summary>
    /// <param name="message">The failed message.</param>
    /// <param name="reason">The reason for the failure.</param>
    /// <returns>A task representing the asynchronous enqueue operation.</returns>
    Task EnqueueAsync(QueueMessage message, string reason);

    /// <summary>
    /// Dequeues the next message from the dead letter queue.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The dequeued message, or null if the queue is empty.</returns>
    Task<QueueMessage> DequeueAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all messages currently in the dead letter queue.
    /// </summary>
    /// <returns>A collection of all dead letter messages.</returns>
    Task<IEnumerable<QueueMessage>> GetAllAsync();

    /// <summary>
    /// Retries processing of a specific message by moving it back to the main queue.
    /// </summary>
    /// <param name="messageId">The ID of the message to retry.</param>
    /// <returns>A task representing the asynchronous retry operation.</returns>
    Task RetryAsync(string messageId);

    /// <summary>
    /// Clears all messages from the dead letter queue.
    /// </summary>
    /// <returns>A task representing the asynchronous clear operation.</returns>
    Task ClearAsync();
}
