using System.ComponentModel;
using Spectre.Console.Cli;

namespace Thalamus.Cli.Commands.Settings;

/// <summary>
///     Settings for the dashboard command.
/// </summary>
public class DashboardSettings : CommandSettings
{
    [CommandOption("--url")]
    [Description("Specify the dashboard URL. Empty value defaults to 'localhost:5055'")]
    public string? Url { get; set; }
}