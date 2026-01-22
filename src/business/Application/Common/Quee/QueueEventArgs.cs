namespace Application.Common.Quee;

/// <summary>
/// Event arguments for queue message received events.
/// </summary>
public class QueueMessageReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the received message.
    /// </summary>
    public QueueMessage Message { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the queue from which the message was received.
    /// </summary>
    public string QueueName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the message was received.
    /// </summary>
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets a value indicating whether the message has been acknowledged.
    /// </summary>
    public bool Acknowledged { get; set; }
}

/// <summary>
/// Event arguments for queue connected events.
/// </summary>
public class QueueConnectedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the connection options used for the connection.
    /// </summary>
    public QueueConnectionOptions ConnectionOptions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the timestamp when the connection was established.
    /// </summary>
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets a value indicating whether this is a reconnection.
    /// </summary>
    public bool IsReconnection { get; set; }

    /// <summary>
    /// Gets or sets the connection attempt number.
    /// </summary>
    public int ConnectionAttempt { get; set; } = 1;
}

/// <summary>
/// Event arguments for queue disconnected events.
/// </summary>
public class QueueDisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the reason for disconnection.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the disconnection occurred.
    /// </summary>
    public DateTime DisconnectedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets a value indicating whether the disconnection was initiated by the client.
    /// </summary>
    public bool InitiatedByClient { get; set; }

    /// <summary>
    /// Gets or sets the exception that caused the disconnection, if any.
    /// </summary>
    public Exception? Exception { get; set; }
}

/// <summary>
/// Event arguments for queue connection state changed events.
/// </summary>
public class QueueConnectionStateChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the previous connection state.
    /// </summary>
    public QueueConnectionState PreviousState { get; set; }

    /// <summary>
    /// Gets or sets the current connection state.
    /// </summary>
    public QueueConnectionState CurrentState { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the state changed.
    /// </summary>
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the reason for the state change.
    /// </summary>
    public string? Reason { get; set; }
}
