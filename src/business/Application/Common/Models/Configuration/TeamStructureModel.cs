using System.Text.Json.Serialization;

namespace Application.Common.Models.Configuration;

/// <summary>
///     Team structure configuration for agent collaboration.
/// </summary>
public class TeamStructureModel
{
	/// <summary>
	///     Gets or sets the description of the team structure.
	/// </summary>
	[JsonPropertyName("description")]
    public string Description { get; set; } = null!;

	/// <summary>
	///     Gets or sets the requirement for a lead agent.
	/// </summary>
	[JsonPropertyName("lead_agent_required")]
    public string LeadAgentRequired { get; set; } = null!;
}