namespace Application.Common.Quee;

/// <summary>
/// Metrics and statistics for a queue.
/// </summary>
public class QueueMetrics
{
    /// <summary>
    /// Gets or sets the name of the queue.
    /// </summary>
    public string QueueName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current depth (number of messages) in the queue.
    /// </summary>
    public long QueueDepth { get; set; }

    /// <summary>
    /// Gets or sets the number of active consumers.
    /// </summary>
    public int ConsumerCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of messages enqueued.
    /// </summary>
    public long TotalEnqueued { get; set; }

    /// <summary>
    /// Gets or sets the total number of messages dequeued.
    /// </summary>
    public long TotalDequeued { get; set; }

    /// <summary>
    /// Gets or sets the total number of messages acknowledged.
    /// </summary>
    public long TotalAcknowledged { get; set; }

    /// <summary>
    /// Gets or sets the total number of messages rejected.
    /// </summary>
    public long TotalRejected { get; set; }

    /// <summary>
    /// Gets or sets the average processing time in milliseconds.
    /// </summary>
    public double AverageProcessingTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the oldest message in the queue.
    /// </summary>
    public DateTime? OldestMessageTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the metrics were collected.
    /// </summary>
    public DateTime CollectedAt { get; set; } = DateTime.UtcNow;
}
