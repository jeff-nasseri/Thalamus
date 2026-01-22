namespace Application.Common.Quee;

/// <summary>
/// Interface for storing and retrieving queue messages from persistent storage.
/// </summary>
public interface IQueueMessageStore
{
    /// <summary>
    /// Stores a message in the message store.
    /// </summary>
    /// <param name="queueName">The name of the queue the message belongs to.</param>
    /// <param name="message">The message to store.</param>
    /// <returns>A task representing the asynchronous store operation.</returns>
    Task StoreAsync(string queueName, QueueMessage message);

    /// <summary>
    /// Retrieves a message by its ID.
    /// </summary>
    /// <param name="messageId">The ID of the message to retrieve.</param>
    /// <returns>The message, or null if not found.</returns>
    Task<QueueMessage> GetAsync(string messageId);

    /// <summary>
    /// Removes a message from the store.
    /// </summary>
    /// <param name="messageId">The ID of the message to remove.</param>
    /// <returns>A task representing the asynchronous remove operation.</returns>
    Task RemoveAsync(string messageId);

    /// <summary>
    /// Gets all messages for a specific queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <returns>A collection of messages belonging to the queue.</returns>
    Task<IEnumerable<QueueMessage>> GetByQueueAsync(string queueName);

    /// <summary>
    /// Clears all messages from the store.
    /// </summary>
    /// <returns>A task representing the asynchronous clear operation.</returns>
    Task ClearAsync();
}
