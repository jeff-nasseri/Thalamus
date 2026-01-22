namespace Application.Common.Quee;

/// <summary>
/// Interface for producing and publishing messages to queues.
/// </summary>
public interface IQueueProducer
{
    /// <summary>
    /// Enqueues a message with binary payload to the specified queue.
    /// </summary>
    /// <param name="queueName">The name of the target queue.</param>
    /// <param name="payload">The binary payload to send.</param>
    /// <param name="deliveryMode">The delivery guarantee mode.</param>
    /// <param name="persistent">Whether the message should persist across restarts.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous enqueue operation.</returns>
    Task EnqueueAsync(
        string queueName,
        byte[] payload,
        QueueDeliveryMode deliveryMode = QueueDeliveryMode.AtMostOnce,
        bool persistent = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Enqueues a message with string payload to the specified queue.
    /// </summary>
    /// <param name="queueName">The name of the target queue.</param>
    /// <param name="payload">The string payload to send.</param>
    /// <param name="deliveryMode">The delivery guarantee mode.</param>
    /// <param name="persistent">Whether the message should persist across restarts.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous enqueue operation.</returns>
    Task EnqueueAsync(
        string queueName,
        string payload,
        QueueDeliveryMode deliveryMode = QueueDeliveryMode.AtMostOnce,
        bool persistent = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Enqueues multiple messages in a single batch operation.
    /// </summary>
    /// <param name="queueName">The name of the target queue.</param>
    /// <param name="messages">The collection of messages to enqueue.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous batch enqueue operation.</returns>
    Task EnqueueBatchAsync(
        string queueName,
        IEnumerable<QueueMessage> messages,
        CancellationToken cancellationToken = default);
}
