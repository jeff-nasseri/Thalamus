using Infrastructure.Services.Cli.Thalamus;
using Spectre.Console;
using Spectre.Console.Cli;
using Thalamus.Cli.Commands.Settings;

namespace Thalamus.Cli.Commands;

/// <summary>
///     Command to setup and manage the web dashboard UI.
/// </summary>
public class DashboardCommand : AsyncCommand<DashboardSettings>
{
    private readonly IThalamusCliService _cliService;

    public DashboardCommand(IThalamusCliService cliService)
    {
        _cliService = cliService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, DashboardSettings settings,
        CancellationToken cancellationToken = default)
    {
        var args = new List<string>();

        if (!string.IsNullOrWhiteSpace(settings.Url))
        {
            args.Add("--url");
            args.Add(settings.Url);
        }

        var response = await _cliService.SetupWebDashboardAsync(args.ToArray());

        if (response.Success)
        {
            AnsiConsole.MarkupLine("[green]Dashboard setup completed successfully.[/]");
            if (response.Data != null) AnsiConsole.WriteLine(response.Data.ToString() ?? "");
            return 0;
        }

        AnsiConsole.MarkupLine("[red]Dashboard setup failed.[/]");
        if (response.Error != null)
            AnsiConsole.MarkupLine(
                $"[red]Error Code: {response.Error.Value.Code}, Type: {response.Error.Value.Type}[/]");
        return 1;
    }
}