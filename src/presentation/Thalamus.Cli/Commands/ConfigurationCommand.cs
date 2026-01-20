using Infrastructure.Services.Cli.Thalamus;
using Spectre.Console;
using Spectre.Console.Cli;
using Thalamus.Cli.Commands.Settings;

namespace Thalamus.Cli.Commands;

/// <summary>
///     Command to manage Thalamus configuration settings.
/// </summary>
public class ConfigurationCommand : AsyncCommand<ConfigurationSettings>
{
    private readonly IThalamusCliService _cliService;

    public ConfigurationCommand(IThalamusCliService cliService)
    {
        _cliService = cliService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ConfigurationSettings settings,
        CancellationToken cancellationToken = default)
    {
        if (settings.Initialization)
        {
            var args = BuildInitializationArgs(settings);
            var response = await _cliService.IdempotentConfigurationInitializationAsync(args);

            if (response.Success)
            {
                AnsiConsole.MarkupLine("[green]Configuration initialization completed successfully.[/]");
                if (response.Data != null) AnsiConsole.WriteLine(response.Data.ToString() ?? "");
                return 0;
            }

            AnsiConsole.MarkupLine("[red]Configuration initialization failed.[/]");
            if (response.Error != null)
                AnsiConsole.MarkupLine(
                    $"[red]Error Code: {response.Error.Value.Code}, Type: {response.Error.Value.Type}[/]");
            return 1;
        }

        AnsiConsole.MarkupLine("[yellow]No configuration action specified. Use --initialization flag.[/]");
        return 0;
    }

    private static string[] BuildInitializationArgs(ConfigurationSettings settings)
    {
        var args = new List<string>();

        if (!string.IsNullOrWhiteSpace(settings.McpPath))
        {
            args.Add("--mcp");
            args.Add(settings.McpPath);
        }

        if (!string.IsNullOrWhiteSpace(settings.AgentPath))
        {
            args.Add("--agent");
            args.Add(settings.AgentPath);
        }

        if (!string.IsNullOrWhiteSpace(settings.NodeIdentifier))
        {
            args.Add("--node");
            args.Add(settings.NodeIdentifier);
        }
        else if (settings.Initialization)
        {
            // Empty node defaults to --all
            args.Add("--node");
        }

        return args.ToArray();
    }
}