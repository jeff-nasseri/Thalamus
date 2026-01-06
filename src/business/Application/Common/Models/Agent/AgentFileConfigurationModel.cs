using System.Text.Json.Serialization;
using Application.Common.Enums;
using Application.Common.Models.Configuration;

namespace Application.Common.Models.Agent;

/// <summary>
///     Configuration model for agent pool settings.
///     Maps to the agent-pool.json configuration file structure.
/// </summary>
public class AgentFileConfigurationModel : FileConfigurationModel
{
	/// <summary>
	///     Gets the configuration model type.
	/// </summary>
	public override ConfigurationModelType ConfigurationModelType { get; } = ConfigurationModelType.SETUP_AGENTS_JSON;

	/// <summary>
	///     Gets or sets the collection of agent definitions.
	/// </summary>
	[JsonPropertyName("agents")]
    public List<AgentDefinitionModel> Agents { get; set; } = new();
}