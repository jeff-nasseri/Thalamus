# Thalamus

A distributed multi-agent orchestration platform built with .NET, designed to coordinate intelligent agents across
master-slave architecture for scalable AI application deployment.

## Overview

Thalamus provides a robust framework for deploying and managing AI agents in a distributed environment. It features a
master node for coordination and slave agents for distributed workload execution.

## Features

- **Multi-Agent Architecture**: Coordinate multiple AI agents with distinct capabilities
- **Distributed Deployment**: Master-slave architecture for scalable operations
- **Memory Management**: Persistent memory threads with keyword-based retrieval
- **Plan Execution**: Strategic task planning and execution framework
- **RESTful API**: Clean API interface for agent interaction
- **Health Monitoring**: Built-in storage and system health checks

## Architecture

The project follows a clean architecture pattern with clear separation of concerns:

```
src/
 business/
    Application/     # Application logic and use cases
    Domain/          # Core domain entities and business rules
    ErrorHandling/   # Centralized error handling
 data-access/
    Infrastructure/  # External services and data persistence
 presentation/
     Web.Api/         # REST API endpoints
     Web.Common/      # Shared web utilities
     Thalamus.Cli/    # Command-line interface
```

## Getting Started

### Prerequisites

- .NET 8.0 or later
- SQL Server (or compatible database)
- Docker (optional, for containerized deployment)

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Thalamus
   ```

2. Configure the environment:
   ```bash
   cp configuration/agent-pool.json.tmpl configuration/agent-pool.json
   cp configuration/mcp-pool.json.tmpl configuration/mcp-pool.json
   ```

3. Build the solution:
   ```bash
   dotnet build Thalamus.sln
   ```

4. Run the Web API:
   ```bash
   cd src/presentation/Web.Api
   dotnet run
   ```

### Configuration

Configuration files are located in the `configuration/` directory:

- `agent-pool.json`: Configure available agents and their capabilities
- `mcp-pool.json`: Configure MCP (Model Context Protocol) settings

## Usage

### Running the API

```bash
dotnet run --project src/presentation/Web.Api
```

The API will be available at `https://localhost:5001` (or configured port).

### Running the CLI

The Thalamus CLI provides a rich command-line interface for managing configuration, executing prompts, and accessing memory.

```bash
# Run the CLI
dotnet run --project src/presentation/Thalamus.Cli -- [command] [options]

# Examples:
dotnet run --project src/presentation/Thalamus.Cli -- configuration --initialization --node
dotnet run --project src/presentation/Thalamus.Cli -- prompt -m "Hello agent"
dotnet run --project src/presentation/Thalamus.Cli -- memory --keywords important
dotnet run --project src/presentation/Thalamus.Cli -- dashboard --url localhost:8080
```

For detailed CLI documentation, see [Thalamus CLI README](src/presentation/Thalamus.Cli/README.md).

## Development

### Project Structure

- **Domain Layer**: Contains core business entities (Agent, Memory, Plan, User, etc.)
- **Application Layer**: Implements use cases and application logic
- **Infrastructure Layer**: Handles external concerns (database, services)
- **Presentation Layer**: API controllers and CLI interface

### Key Components

- **Agents**: Autonomous entities that can execute tasks
- **Memory System**: Thread-based memory with keyword indexing
- **Plans**: Strategic execution plans with result tracking
- **Value Objects**: Email, Messages, Tasks, and Keywords

## Testing

Run tests using:

```bash
dotnet test
```

## Docker Support

Build and run using Docker:

```bash
docker build -f src/presentation/Web.Api/Dockerfile -t thalamus-api .
docker run -p 5000:8080 thalamus-api
```

## Documentation

- [Architecture Documentation](docs/README.md)
- [Contributing Guidelines](CONTRIBUTE.md)
- [Security Policy](SECURITY.md)
- [Feature Specifications](feature/)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Support

For issues and feature requests, please use the GitHub issue tracker.

## Roadmap

See [TODO.txt](TODO.txt) for planned features and improvements.
