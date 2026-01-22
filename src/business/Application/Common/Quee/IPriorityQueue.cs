namespace Application.Common.Quee;

/// <summary>
/// Interface for priority-based queue operations where messages are processed
/// based on their priority level.
/// </summary>
public interface IPriorityQueue
{
    /// <summary>
    /// Enqueues a message with a specific priority level.
    /// </summary>
    /// <param name="message">The message to enqueue.</param>
    /// <param name="priority">The priority level (higher values indicate higher priority).</param>
    /// <returns>A task representing the asynchronous enqueue operation.</returns>
    Task EnqueueAsync(QueueMessage message, int priority);

    /// <summary>
    /// Dequeues the message with the highest priority.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The highest priority message, or null if the queue is empty.</returns>
    Task<QueueMessage> DequeueHighestPriorityAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all messages with a specific priority level.
    /// </summary>
    /// <param name="priority">The priority level to filter by.</param>
    /// <returns>A collection of messages with the specified priority.</returns>
    Task<IEnumerable<QueueMessage>> GetByPriorityAsync(int priority);
}
