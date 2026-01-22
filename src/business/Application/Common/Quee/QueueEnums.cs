namespace Application.Common.Quee;

/// <summary>
/// Defines the delivery guarantee modes for queue messages.
/// </summary>
public enum QueueDeliveryMode
{
    /// <summary>
    /// Fire and forget - no delivery guarantee.
    /// </summary>
    AtMostOnce = 0,

    /// <summary>
    /// Acknowledged delivery - guarantees at least one delivery.
    /// </summary>
    AtLeastOnce = 1,

    /// <summary>
    /// Guaranteed single delivery - ensures exactly one delivery.
    /// </summary>
    ExactlyOnce = 2
}

/// <summary>
/// Represents the current state of a queue connection.
/// </summary>
public enum QueueConnectionState
{
    /// <summary>
    /// The connection is not established.
    /// </summary>
    Disconnected,

    /// <summary>
    /// The connection is in the process of being established.
    /// </summary>
    Connecting,

    /// <summary>
    /// The connection is successfully established.
    /// </summary>
    Connected,

    /// <summary>
    /// The connection is attempting to reconnect after a failure.
    /// </summary>
    Reconnecting,

    /// <summary>
    /// The connection is in the process of being closed.
    /// </summary>
    Disconnecting
}
