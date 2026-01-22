namespace Application.Common.Quee;

/// <summary>
/// Interface for monitoring queue health, metrics, and operational statistics.
/// </summary>
public interface IQueueMonitor
{
    /// <summary>
    /// Gets comprehensive metrics for a specific queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to get metrics for.</param>
    /// <returns>The metrics for the specified queue.</returns>
    Task<QueueMetrics> GetMetricsAsync(string queueName);

    /// <summary>
    /// Gets the current depth (number of messages) in a queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <returns>The number of messages in the queue.</returns>
    Task<long> GetQueueDepthAsync(string queueName);

    /// <summary>
    /// Gets a list of all currently active queues.
    /// </summary>
    /// <returns>A collection of active queue names.</returns>
    Task<IEnumerable<string>> GetActiveQueuesAsync();

    /// <summary>
    /// Gets the number of active consumers for a specific queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <returns>The number of active consumers.</returns>
    Task<int> GetConsumerCountAsync(string queueName);
}
