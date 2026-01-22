namespace Application.Common.Quee;

/// <summary>
/// Configuration options for establishing a queue connection.
/// </summary>
public class QueueConnectionOptions
{
    /// <summary>
    /// Gets or sets the host address of the queue server.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// Gets or sets the port number for the queue server.
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password for authentication.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the virtual host for the connection.
    /// </summary>
    public string VirtualHost { get; set; } = "/";

    /// <summary>
    /// Gets or sets a value indicating whether to use SSL/TLS for the connection.
    /// </summary>
    public bool UseSsl { get; set; }

    /// <summary>
    /// Gets or sets the connection timeout in milliseconds.
    /// </summary>
    public int ConnectionTimeout { get; set; } = 30000;

    /// <summary>
    /// Gets or sets the heartbeat interval in seconds.
    /// </summary>
    public ushort HeartbeatInterval { get; set; } = 60;

    /// <summary>
    /// Gets or sets a value indicating whether to automatically recover from connection failures.
    /// </summary>
    public bool AutomaticRecoveryEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the interval between reconnection attempts in milliseconds.
    /// </summary>
    public int NetworkRecoveryInterval { get; set; } = 5000;

    /// <summary>
    /// Gets or sets additional connection properties.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new();
}
