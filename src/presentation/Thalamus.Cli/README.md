# Thalamus CLI

Thalamus CLI is a command-line interface for managing and interacting with the Thalamus agent management system. Built
with Spectre.Console, it provides a modern, user-friendly terminal experience.

## Quick Start

```bash
# Build the CLI
dotnet build

# Run the CLI
dotnet run -- [command] [options]

# Display help
dotnet run -- --help
```

## Commands Overview

### Configuration Management

Initialize and manage Thalamus configuration settings.

```bash
# Initialize all configurations (MCP, agents, and nodes)
dotnet run -- configuration --initialization --node

# Initialize specific MCP configuration
dotnet run -- configuration -i --mcp path/to/mcp-config.json

# Initialize specific agent and node configurations
dotnet run -- configuration -i --agent path/to/agent-config.json --node node-123
```

**Options:**

- `-i, --initialization` - Initialize configuration in data storage
- `-m, --mcp <path>` - Initialize MCP (Model Context Protocol) configuration from file
- `-a, --agent <path>` - Initialize agent configuration from file
- `-n, --node <identifier>` - Initialize node configuration (empty value defaults to all nodes)

### Dashboard Management

Setup and manage the web dashboard UI.

```bash
# Setup dashboard with default URL (localhost:5055)
dotnet run -- dashboard

# Setup dashboard with custom URL
dotnet run -- dashboard --url localhost:8080
```

**Options:**

- `--url <url>` - Specify the dashboard URL (defaults to 'localhost:5055')

### Memory Management

Access and manage Thalamus application memory.

```bash
# Retrieve all memory entries
dotnet run -- memory --filter all

# Filter memory by keywords
dotnet run -- memory --keywords keyword1,keyword2

# Filter memory by title
dotnet run -- memory --title "My Title"

# Export memory to JSON file
dotnet run -- memory --export output.json --format json

# Export filtered memory
dotnet run -- memory --keywords important --export filtered.json --format json
```

**Options:**

- `--filter <criteria>` - Filter memory entries (defaults to 'all')
- `--keywords <keywords>` - Filter by comma-separated keywords
- `--title <title>` - Filter by title
- `--export <path>` - Export memory data to file
- `--format <format>` - Specify export format (json)

### Prompt Execution

Execute prompts and communicate with the agent.

```bash
# Execute a prompt synchronously (waits for completion)
dotnet run -- prompt --message "Hello agent"

# Execute a prompt in the background (returns immediately)
dotnet run -- prompt -m "Process this task" --background
```

**Options:**

- `-m, --message <text>` - The prompt message to execute (required)
- `-b, --background` - Run prompt as background job (defaults to synchronous execution)

## Configuration Files

The CLI expects configuration files in the application's base directory:

- `agent-pool.json` - Agent configuration pool
- `mcp-pool.json` - MCP plugin configuration pool
- `node-pool.json` - Node configuration pool

### Example Configuration Structure

**agent-pool.json:**

```json
{
  "Agents": [
    {
      "Id": "agent-1",
      "Name": "Primary Agent",
      "Type": "master"
    }
  ]
}
```

**mcp-pool.json:**

```json
{
  "Plugins": [
    {
      "Id": "mcp-plugin-1",
      "Name": "Default MCP Plugin",
      "Endpoint": "https://api.example.com"
    }
  ]
}
```

**node-pool.json:**

```json
{
  "Nodes": [
    {
      "Id": "node-1",
      "Name": "Primary Node",
      "Status": "active"
    }
  ]
}
```

## Architecture

### Technology Stack

- **Spectre.Console** - Modern, cross-platform terminal UI framework
- **Spectre.Console.Cli** - Command-line parsing and routing
- **MediatR** - Mediator pattern implementation for CQRS
- **Microsoft.Extensions.DependencyInjection** - Dependency injection container

### Project Structure

```
Thalamus.Cli/
├── Commands/               # Command implementations
│   ├── ConfigurationCommand.cs
│   ├── DashboardCommand.cs
│   ├── MemoryCommand.cs
│   ├── PromptCommand.cs
│   └── Settings/          # Command settings (options/arguments)
│       ├── ConfigurationSettings.cs
│       ├── DashboardSettings.cs
│       ├── MemorySettings.cs
│       └── PromptSettings.cs
├── Infrastructure/         # DI and infrastructure setup
│   ├── ServiceCollectionExtensions.cs
│   ├── TypeRegistrar.cs
│   └── TypeResolver.cs
├── Program.cs             # Application entry point
└── thalamus-cli-structure.json  # CLI structure definition
```

### Dependency Flow

```
Program.cs
  ↓
ServiceCollection (DI Container)
  ↓
Application Layer (MediatR handlers)
  ↓
Infrastructure Services (CLI services)
  ↓
Commands (ConfigurationCommand, PromptCommand, etc.)
```

## Development Guide

### Adding a New Command

1. **Define the command structure** in `thalamus-cli-structure.json`

2. **Create a settings class** in `Commands/Settings/`:

```csharp
using System.ComponentModel;
using Spectre.Console.Cli;

namespace Thalamus.Cli.Commands.Settings;

public class MyCommandSettings : CommandSettings
{
    [CommandOption("-o|--option")]
    [Description("Description of the option")]
    public string? Option { get; set; }

    public override ValidationResult Validate()
    {
        // Add custom validation if needed
        return ValidationResult.Success();
    }
}
```

3. **Create a command class** in `Commands/`:

```csharp
using Infrastructure.Services.Cli.Thalamus;
using Spectre.Console;
using Spectre.Console.Cli;
using Thalamus.Cli.Commands.Settings;

namespace Thalamus.Cli.Commands;

public class MyCommand : AsyncCommand<MyCommandSettings>
{
    private readonly IThalamusCliService _cliService;

    public MyCommand(IThalamusCliService cliService)
    {
        _cliService = cliService;
    }

    public override async Task<int> ExecuteAsync(
        CommandContext context,
        MyCommandSettings settings,
        CancellationToken cancellationToken = default)
    {
        // Command logic here
        AnsiConsole.MarkupLine("[green]Success![/]");
        return 0; // Success
    }
}
```

4. **Register the command** in `Program.cs`:

```csharp
config.AddCommand<MyCommand>("mycommand")
    .WithDescription("Description of my command")
    .WithExample(new[] { "mycommand", "--option", "value" });
```

### Error Handling

The CLI uses a structured error handling system from the ErrorHandling project:

```csharp
var response = await _cliService.SomeOperationAsync(args);

if (response.Success)
{
    AnsiConsole.MarkupLine("[green]Operation successful![/]");
    return 0;
}

AnsiConsole.MarkupLine("[red]Operation failed.[/]");
if (response.Error != null)
{
    AnsiConsole.MarkupLine(
        $"[red]Error Code: {response.Error.Value.Code}, " +
        $"Type: {response.Error.Value.Type}[/]"
    );
}
return 1;
```

### Styling Output

Use Spectre.Console markup for rich terminal output:

```csharp
// Colors
AnsiConsole.MarkupLine("[green]Success message[/]");
AnsiConsole.MarkupLine("[red]Error message[/]");
AnsiConsole.MarkupLine("[yellow]Warning message[/]");

// Styles
AnsiConsole.MarkupLine("[bold]Bold text[/]");
AnsiConsole.MarkupLine("[italic]Italic text[/]");
AnsiConsole.MarkupLine("[underline]Underlined text[/]");

// Tables
var table = new Table();
table.AddColumn("Header 1");
table.AddColumn("Header 2");
table.AddRow("Value 1", "Value 2");
AnsiConsole.Write(table);

// Progress bars
await AnsiConsole.Progress()
    .StartAsync(async ctx =>
    {
        var task = ctx.AddTask("Processing");
        while (!ctx.IsFinished)
        {
            await Task.Delay(100);
            task.Increment(1.5);
        }
    });
```

## Testing

### Manual Testing

```bash
# Test configuration initialization
dotnet run -- configuration --initialization --node

# Test prompt execution
dotnet run -- prompt -m "Test message"

# Test help system
dotnet run -- --help
dotnet run -- configuration --help
```

### Integration Testing

The CLI integrates with the Infrastructure and Application layers, so ensure those services are properly configured
before testing.

## Troubleshooting

### Common Issues

**Issue**: "Configuration file not found"

- **Solution**: Ensure `agent-pool.json`, `mcp-pool.json`, and `node-pool.json` exist in the application's base
  directory

**Issue**: "Command not recognized"

- **Solution**: Check that the command is registered in `Program.cs` and matches the syntax

**Issue**: "Dependency injection error"

- **Solution**: Verify all services are registered in `ServiceCollectionExtensions.cs`

### Debug Mode

Run with verbose logging:

```bash
dotnet run --verbosity detailed -- [command]
```

## Performance Considerations

- Commands are executed asynchronously to support long-running operations
- Background mode (`--background`) allows prompt execution without blocking
- Memory operations can be filtered to reduce data transfer

## Security

- The CLI does not store credentials
- Configuration files should be kept secure and not committed to version control
- API endpoints should be protected with appropriate authentication

## Contributing

When adding new features:

1. Follow the existing command structure pattern
2. Add comprehensive examples to this README
3. Include validation in settings classes
4. Use Spectre.Console for all user-facing output
5. Handle errors gracefully with user-friendly messages

## Related Documentation

- [Main Thalamus README](../../../README.md)
- [Infrastructure Services](../../data-access/Infrastructure/README.md)
- [Spectre.Console Documentation](https://spectreconsole.net/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)

## License

This project is part of the Thalamus platform. See the main [LICENSE](../../../LICENSE.md) file for details.
