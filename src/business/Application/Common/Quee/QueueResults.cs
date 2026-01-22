namespace Application.Common.Quee;

/// <summary>
/// Result of an enqueue operation.
/// </summary>
public class EnqueueResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the enqueue operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the message ID of the enqueued message.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Gets or sets an error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was enqueued.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Result of a dequeue operation.
/// </summary>
public class DequeueResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the dequeue operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the dequeued message.
    /// </summary>
    public QueueMessage? Message { get; set; }

    /// <summary>
    /// Gets or sets an error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was dequeued.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Result of an unsubscribe operation.
/// </summary>
public class UnsubscribeResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the unsubscribe operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the name of the queue that was unsubscribed.
    /// </summary>
    public string? QueueName { get; set; }

    /// <summary>
    /// Gets or sets an error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the unsubscribe operation occurred.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
