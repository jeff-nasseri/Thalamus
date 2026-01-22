namespace Application.Common.Quee;

/// <summary>
/// Interface for handling and processing queue messages with routing capabilities.
/// </summary>
public interface IQueueMessageHandler
{
    /// <summary>
    /// Handles the processing of a queue message.
    /// </summary>
    /// <param name="message">The message to handle.</param>
    /// <returns>A task representing the asynchronous handling operation.</returns>
    Task HandleAsync(QueueMessage message);

    /// <summary>
    /// Determines whether this handler can process messages from the specified queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <returns>True if this handler can process messages from the queue; otherwise, false.</returns>
    bool CanHandle(string queueName);

    /// <summary>
    /// Processes a message and returns the processing result.
    /// </summary>
    /// <param name="message">The message to process.</param>
    /// <returns>True if processing was successful; otherwise, false.</returns>
    Task<bool> ProcessAsync(QueueMessage message);
}
