using System.ComponentModel;
using Spectre.Console.Cli;

namespace Thalamus.Cli.Commands.Settings;

/// <summary>
///     Settings for the memory command.
/// </summary>
public class MemorySettings : CommandSettings
{
    [CommandOption("--filter")]
    [Description("Filter memory entries. Empty value defaults to '--all'")]
    public string? Filter { get; set; }

    [CommandOption("--keywords")]
    [Description("Filter by keywords")]
    public string? Keywords { get; set; }

    [CommandOption("--title")]
    [Description("Filter by title")]
    public string? Title { get; set; }

    [CommandOption("--export")]
    [Description("Export memory data to file")]
    public string? ExportPath { get; set; }

    [CommandOption("--format")]
    [Description("Specify export format (json). Empty value defaults to all formats")]
    public string? Format { get; set; }
}