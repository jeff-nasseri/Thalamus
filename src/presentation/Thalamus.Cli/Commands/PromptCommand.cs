using Infrastructure.Services.Cli.Thalamus;
using Spectre.Console;
using Spectre.Console.Cli;
using Thalamus.Cli.Commands.Settings;

namespace Thalamus.Cli.Commands;

/// <summary>
///     Command to execute prompts and communicate with the agent.
/// </summary>
public class PromptCommand : AsyncCommand<PromptSettings>
{
    private readonly IThalamusCliService _cliService;

    public PromptCommand(IThalamusCliService cliService)
    {
        _cliService = cliService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, PromptSettings settings,
        CancellationToken cancellationToken = default)
    {
        var args = new List<string>
        {
            "--message",
            settings.Message!
        };

        if (settings.Background) args.Add("--background");

        var response = await _cliService.ExecutePromptAsync(args.ToArray());

        if (response.Success)
        {
            AnsiConsole.MarkupLine("[green]Prompt executed successfully.[/]");
            if (response.Data != null) AnsiConsole.WriteLine(response.Data.ToString() ?? "");
            return 0;
        }

        AnsiConsole.MarkupLine("[red]Failed to execute prompt.[/]");
        if (response.Error != null)
            AnsiConsole.MarkupLine(
                $"[red]Error Code: {response.Error.Value.Code}, Type: {response.Error.Value.Type}[/]");
        return 1;
    }
}