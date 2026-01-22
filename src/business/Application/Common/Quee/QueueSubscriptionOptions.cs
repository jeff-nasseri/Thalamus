namespace Application.Common.Quee;

/// <summary>
/// Options for subscribing to queue messages.
/// </summary>
public class QueueSubscriptionOptions
{
    /// <summary>
    /// Gets or sets the name of the queue to subscribe to.
    /// </summary>
    public string QueueName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the delivery mode for received messages.
    /// </summary>
    public QueueDeliveryMode DeliveryMode { get; set; } = QueueDeliveryMode.AtMostOnce;

    /// <summary>
    /// Gets or sets a value indicating whether to automatically acknowledge messages.
    /// </summary>
    public bool AutoAcknowledge { get; set; } = true;

    /// <summary>
    /// Gets or sets the prefetch count (number of messages to fetch at once).
    /// </summary>
    public ushort PrefetchCount { get; set; } = 1;

    /// <summary>
    /// Gets or sets a value indicating whether the subscription is exclusive.
    /// </summary>
    public bool Exclusive { get; set; }

    /// <summary>
    /// Gets or sets the consumer tag for the subscription.
    /// </summary>
    public string? ConsumerTag { get; set; }

    /// <summary>
    /// Gets or sets additional subscription arguments.
    /// </summary>
    public Dictionary<string, object> Arguments { get; set; } = new();
}
