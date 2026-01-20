using Application;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Thalamus.Cli.Commands;
using Thalamus.Cli.Infrastructure;

namespace Thalamus.Cli;

public class Program
{
    public static int Main(string[] args)
    {
        // Setup dependency injection
        var services = new ServiceCollection();

        // Add Application layer (MediatR, validators, etc.)
        services.AddApplication();

        // Add CLI services
        services.AddThalamusCliServices();

        // Create type registrar for Spectre.Console
        var registrar = new TypeRegistrar(services);

        // Create and configure command app
        var app = new CommandApp(registrar);

        app.Configure(config =>
        {
            config.SetApplicationName("thalamus");
            config.SetApplicationVersion("1.0.0");

            config.ValidateExamples();

            // Add root commands
            config.AddCommand<ConfigurationCommand>("configuration")
                .WithDescription("Manage Thalamus configuration settings")
                .WithExample("configuration", "--initialization")
                .WithExample("configuration", "-i", "--mcp", "path/to/mcp-config.json")
                .WithExample("configuration", "-i", "--agent", "path/to/agent-config.json", "--node", "node-123");

            config.AddCommand<DashboardCommand>("dashboard")
                .WithDescription("Setup and manage the web dashboard UI")
                .WithExample("dashboard")
                .WithExample("dashboard", "--url", "localhost:8080");

            config.AddCommand<MemoryCommand>("memory")
                .WithDescription("Access and manage Thalamus application memory")
                .WithExample("memory", "--filter", "all")
                .WithExample("memory", "--keywords", "keyword1,keyword2")
                .WithExample("memory", "--title", "My Title")
                .WithExample("memory", "--export", "output.json", "--format", "json");

            config.AddCommand<PromptCommand>("prompt")
                .WithDescription("Execute prompts and communicate with the agent")
                .WithExample("prompt", "--message", "Hello agent")
                .WithExample("prompt", "-m", "Process this task", "--background");
        });

        try
        {
            return app.Run(args);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            return 1;
        }
    }
}