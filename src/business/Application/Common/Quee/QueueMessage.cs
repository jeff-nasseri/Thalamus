namespace Application.Common.Quee;

/// <summary>
/// Represents a message in the queue system with metadata and payload.
/// </summary>
public class QueueMessage
{
    /// <summary>
    /// Gets or sets the unique identifier for the message.
    /// </summary>
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the queue this message belongs to.
    /// </summary>
    public string QueueName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message payload as a byte array.
    /// </summary>
    public byte[] Payload { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Gets or sets the delivery mode for the message.
    /// </summary>
    public QueueDeliveryMode DeliveryMode { get; set; } = QueueDeliveryMode.AtMostOnce;

    /// <summary>
    /// Gets or sets a value indicating whether the message should persist across restarts.
    /// </summary>
    public bool Persistent { get; set; }

    /// <summary>
    /// Gets or sets the priority of the message (higher values indicate higher priority).
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the timestamp when the message expires.
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets additional metadata for the message.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new();

    /// <summary>
    /// Gets or sets the number of delivery attempts for this message.
    /// </summary>
    public int DeliveryAttempts { get; set; }
}
