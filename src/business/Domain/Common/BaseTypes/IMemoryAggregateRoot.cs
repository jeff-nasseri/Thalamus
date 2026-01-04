namespace Domain.Common.BaseTypes;

/// <summary>
///     Marker interface for memory-specific aggregate roots in the Thalamus memory subsystem.
///     Entities implementing this interface are part of the agent memory domain, which includes
///     memories, memory threads, prompts, and plans.
/// </summary>
/// <remarks>
///     Memory aggregate roots follow Domain-Driven Design principles and represent the boundaries
///     of consistency within the memory storage system. They are the entry points for accessing
///     and modifying agent conversation history and execution records.
/// </remarks>
public interface IMemoryAggregateRoot : IAggregateRoot
{
}