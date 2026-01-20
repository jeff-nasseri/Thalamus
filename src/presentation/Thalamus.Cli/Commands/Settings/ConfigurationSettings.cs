using System.ComponentModel;
using Spectre.Console.Cli;

namespace Thalamus.Cli.Commands.Settings;

/// <summary>
///     Settings for the configuration command.
/// </summary>
public class ConfigurationSettings : CommandSettings
{
    [CommandOption("-i|--initialization")]
    [Description("Initialize configuration in data storage")]
    public bool Initialization { get; set; }

    [CommandOption("-m|--mcp")]
    [Description("Initialize MCP (Model Context Protocol) configuration")]
    public string? McpPath { get; set; }

    [CommandOption("-a|--agent")]
    [Description("Initialize agent configuration")]
    public string? AgentPath { get; set; }

    [CommandOption("-n|--node")]
    [Description("Initialize node configuration. Empty value defaults to '--all'")]
    public string? NodeIdentifier { get; set; }
}