namespace Application.Common.Quee;

/// <summary>
/// Interface for transactional queue operations supporting atomic message processing.
/// </summary>
public interface IQueueTransaction
{
    /// <summary>
    /// Begins a new transaction for queue operations.
    /// </summary>
    /// <returns>A task representing the asynchronous begin operation.</returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits all operations performed within the current transaction.
    /// </summary>
    /// <returns>A task representing the asynchronous commit operation.</returns>
    Task CommitAsync();

    /// <summary>
    /// Rolls back all operations performed within the current transaction.
    /// </summary>
    /// <returns>A task representing the asynchronous rollback operation.</returns>
    Task RollbackAsync();

    /// <summary>
    /// Dequeues a message within the current transaction.
    /// </summary>
    /// <param name="queueName">The name of the queue to dequeue from.</param>
    /// <returns>The dequeued message within the transaction.</returns>
    Task<QueueMessage> DequeueTransactionalAsync(string queueName);

    /// <summary>
    /// Enqueues a message within the current transaction.
    /// </summary>
    /// <param name="queueName">The name of the queue to enqueue to.</param>
    /// <param name="message">The message to enqueue.</param>
    /// <returns>A task representing the asynchronous transactional enqueue operation.</returns>
    Task EnqueueTransactionalAsync(string queueName, QueueMessage message);
}
