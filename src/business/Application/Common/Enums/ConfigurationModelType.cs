namespace Application.Common.Enums;

/// <summary>
///     Defines the types of configuration models supported by the system.
/// </summary>
public enum ConfigurationModelType
{
    /// <summary>
    ///     MCP pool configuration (mcp-pool.json).
    /// </summary>
    SUPPORTED_MCP_JSON,

    /// <summary>
    ///     Agent pool configuration (agent-pool.json).
    /// </summary>
    SETUP_AGENTS_JSON
}