using Infrastructure.Services.Cli.Thalamus;
using Spectre.Console;
using Spectre.Console.Cli;
using Thalamus.Cli.Commands.Settings;

namespace Thalamus.Cli.Commands;

/// <summary>
///     Command to access and manage Thalamus application memory.
/// </summary>
public class MemoryCommand : AsyncCommand<MemorySettings>
{
    private readonly IThalamusCliService _cliService;

    public MemoryCommand(IThalamusCliService cliService)
    {
        _cliService = cliService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, MemorySettings settings,
        CancellationToken cancellationToken = default)
    {
        // Handle export if specified
        if (!string.IsNullOrWhiteSpace(settings.ExportPath)) return await ExportMemoryAsync(settings);

        // Otherwise, retrieve and display memory
        return await GetMemoryAsync(settings);
    }

    private async Task<int> GetMemoryAsync(MemorySettings settings)
    {
        var args = BuildFilterArgs(settings);
        var response = await _cliService.GetMemoryAsync(args);

        if (response.Success)
        {
            AnsiConsole.MarkupLine("[green]Memory retrieved successfully.[/]");
            if (response.Data != null) AnsiConsole.WriteLine(response.Data.ToString() ?? "");
            return 0;
        }

        AnsiConsole.MarkupLine("[red]Failed to retrieve memory.[/]");
        if (response.Error != null)
            AnsiConsole.MarkupLine(
                $"[red]Error Code: {response.Error.Value.Code}, Type: {response.Error.Value.Type}[/]");
        return 1;
    }

    private async Task<int> ExportMemoryAsync(MemorySettings settings)
    {
        var args = BuildExportArgs(settings);
        var response = await _cliService.ExportMemoryAsync(args);

        if (response.Success)
        {
            AnsiConsole.MarkupLine("[green]Memory exported successfully.[/]");
            if (response.Data != null) AnsiConsole.WriteLine(response.Data.ToString() ?? "");
            return 0;
        }

        AnsiConsole.MarkupLine("[red]Failed to export memory.[/]");
        if (response.Error != null)
            AnsiConsole.MarkupLine(
                $"[red]Error Code: {response.Error.Value.Code}, Type: {response.Error.Value.Type}[/]");
        return 1;
    }

    private static string[] BuildFilterArgs(MemorySettings settings)
    {
        var args = new List<string>();

        if (!string.IsNullOrWhiteSpace(settings.Keywords))
        {
            args.Add("--keywords");
            args.Add(settings.Keywords);
        }

        if (!string.IsNullOrWhiteSpace(settings.Title))
        {
            args.Add("--title");
            args.Add(settings.Title);
        }

        if (!string.IsNullOrWhiteSpace(settings.Filter))
        {
            args.Add("--filter");
            args.Add(settings.Filter);
        }

        if (args.Count == 0) args.Add("--all");

        return args.ToArray();
    }

    private static string[] BuildExportArgs(MemorySettings settings)
    {
        var args = new List<string>();

        args.Add("--path");
        args.Add(settings.ExportPath!);

        if (!string.IsNullOrWhiteSpace(settings.Format))
        {
            args.Add("--format");
            args.Add(settings.Format);
        }

        return args.ToArray();
    }
}