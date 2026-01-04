using Domain.Common.BaseTypes;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
///     Represents a Model Context Protocol (MCP) plugin that extends agent capabilities.
///     Plugins provide additional tools, integrations, and functionality to agents.
/// </summary>
public class McpPlugin : BaseEntity
{
    /// <summary>
    ///     Gets or sets the display title of the plugin.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the unique code identifier for the plugin.
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    ///     Gets or sets a description of the plugin's functionality and purpose.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the configuration value object for this plugin.
    ///     Contains plugin-specific configuration settings including runtime platform and deployment details.
    /// </summary>
    public McpConfigurationValueObject McpConfigurationValueObject { get; set; } = null!;
}