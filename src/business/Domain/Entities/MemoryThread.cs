using Domain.Common.BaseTypes;

namespace Domain.Entities;

/// <summary>
/// Represents a conversation thread within a memory.
/// A thread manages the sequence of prompts, plans, executions, and results.
/// </summary>
public class MemoryThread : BaseEntity
{
    /// <summary>
    /// Gets or sets the title of the memory thread.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of prompts associated with this thread.
    /// </summary>
    public IEnumerable<Prompt> Prompts { get; set; } = null!;
}
