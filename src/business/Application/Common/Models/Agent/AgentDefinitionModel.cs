using System.Text.Json.Serialization;
using Application.Common.Models.Configuration;

namespace Application.Common.Models.Agent;

/// <summary>
///     Definition of an agent in the agent pool.
/// </summary>
public class AgentDefinitionModel
{
	/// <summary>
	///     Gets or sets the agent type (e.g., "master_agent", "slave_agent").
	/// </summary>
	[JsonPropertyName("type")]
    public string Type { get; set; } = null!;

	/// <summary>
	///     Gets or sets the agent location (e.g., "master_node", "slave_node").
	/// </summary>
	[JsonPropertyName("location")]
    public string Location { get; set; } = null!;

	/// <summary>
	///     Gets or sets the agent role description.
	/// </summary>
	[JsonPropertyName("role")]
    public string Role { get; set; } = null!;

	/// <summary>
	///     Gets or sets the collection of agent responsibilities.
	/// </summary>
	[JsonPropertyName("responsibilities")]
    public List<string>? Responsibilities { get; set; }

	/// <summary>
	///     Gets or sets the team structure configuration (for slave agents).
	/// </summary>
	[JsonPropertyName("team_structure")]
    public TeamStructureModel? TeamStructure { get; set; }
}