namespace Application.Common.Quee;

/// <summary>
/// Interface for consuming messages from queues with subscription management
/// and message acknowledgment capabilities.
/// </summary>
public interface IQueueConsumer
{
    /// <summary>
    /// Subscribes to a single queue to receive messages.
    /// </summary>
    /// <param name="queueName">The name of the queue to subscribe to.</param>
    /// <param name="deliveryMode">The delivery guarantee mode.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous subscribe operation.</returns>
    Task SubscribeAsync(
        string queueName,
        QueueDeliveryMode deliveryMode = QueueDeliveryMode.AtMostOnce,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribes to multiple queues simultaneously.
    /// </summary>
    /// <param name="queueNames">The collection of queue names to subscribe to.</param>
    /// <param name="deliveryMode">The delivery guarantee mode.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous subscribe operation.</returns>
    Task SubscribeAsync(
        IEnumerable<string> queueNames,
        QueueDeliveryMode deliveryMode = QueueDeliveryMode.AtMostOnce,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unsubscribes from a single queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to unsubscribe from.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous unsubscribe operation.</returns>
    Task UnsubscribeAsync(
        string queueName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unsubscribes from multiple queues simultaneously.
    /// </summary>
    /// <param name="queueNames">The collection of queue names to unsubscribe from.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous unsubscribe operation.</returns>
    Task UnsubscribeAsync(
        IEnumerable<string> queueNames,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Acknowledges successful processing of a message.
    /// </summary>
    /// <param name="messageId">The ID of the message to acknowledge.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous acknowledge operation.</returns>
    Task AcknowledgeAsync(
        string messageId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Rejects a message and optionally requeues it for later processing.
    /// </summary>
    /// <param name="messageId">The ID of the message to reject.</param>
    /// <param name="requeue">Whether to requeue the message.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous reject operation.</returns>
    Task RejectAsync(
        string messageId,
        bool requeue = false,
        CancellationToken cancellationToken = default);
}
