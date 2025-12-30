using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.BaseTypes;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a prompt within a memory thread.
/// Prompts store messages exchanged between agents and humans, supporting nested conversations.
/// </summary>
/// <remarks>
/// Currently, prompt messages contain only text in any format and do not support file attachments.
/// </remarks>
public class Prompt : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the parent prompt for nested prompt hierarchies.
    /// </summary>
    public string? ParentPromptId { get; set; }

    /// <summary>
    /// Gets or sets the parent prompt reference for nested prompt hierarchies.
    /// </summary>
    [ForeignKey(nameof(ParentPromptId))]
    public Prompt? ParentPrompt { get; set; }

    /// <summary>
    /// Gets or sets the collection of child prompts in a nested hierarchy.
    /// </summary>
    public IEnumerable<Prompt>? Prompts { get; set; }

    /// <summary>
    /// Gets or sets the execution plan associated with this prompt.
    /// The plan can be requested by either an agent or a human.
    /// </summary>
    public Plan? Plan { get; set; }

    /// <summary>
    /// Gets or sets the message content of the prompt.
    /// Messages can originate from humans or agents (agent-to-agent communication).
    /// This enables the system to maintain conversation history across all interaction types.
    /// </summary>
    public MessageValueObject Message { get; set; } = null!;
}
