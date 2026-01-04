using Domain.Common.BaseTypes;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
///     Represents the memory storage of a Thalamus node.
///     Memory stores conversations and plan executions for agents, organized by threads and indexed by keywords.
/// </summary>
public class Memory : BaseEntity, IMemoryAggregateRoot
{
    /// <summary>
    ///     Gets or sets the collection of memory threads.
    ///     Each thread represents a distinct conversation path.
    /// </summary>
    public IEnumerable<MemoryThread> Threads { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the collection of keywords associated with this memory.
    ///     Keywords are used for indexing and retrieving related memories efficiently.
    /// </summary>
    public IEnumerable<MemoryKeywordValueObject> Keywords { get; set; } = null!;
}