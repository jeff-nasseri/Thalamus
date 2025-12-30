# Contributing to Thalamus

Thank you for your interest in contributing to Thalamus! This document provides guidelines and instructions for contributing to the project.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)
- [Testing](#testing)

## Code of Conduct

We are committed to providing a welcoming and inclusive environment. Please be respectful and constructive in all interactions.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/your-username/Thalamus.git
   cd Thalamus
   ```
3. **Add upstream remote**:
   ```bash
   git remote add upstream https://github.com/original-owner/Thalamus.git
   ```
4. **Create a feature branch**:
   ```bash
   git checkout -b feat/your-feature-name
   ```

## Development Workflow

### Setting Up Your Environment

1. Install .NET 8.0 SDK or later
2. Install your preferred IDE (Visual Studio, Rider, or VS Code)
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Build the solution:
   ```bash
   dotnet build
   ```

### Making Changes

1. **Keep changes focused**: One feature or fix per branch
2. **Write tests**: Ensure new code has appropriate test coverage
3. **Update documentation**: Update README or docs if needed
4. **Follow the architecture**: Respect the clean architecture layers

## Coding Standards

### C# Style Guidelines

- Use **PascalCase** for public members, types, and namespaces
- Use **camelCase** for private fields and local variables
- Use **meaningful names** that describe intent
- Follow the existing code style in the project
- Use **implicit typing** (`var`) when the type is obvious
- Keep methods focused and small (single responsibility)

### Project Structure

When adding new features, follow the established architecture:

```
Domain/          - Core business logic, no dependencies
Application/     - Use cases, depends on Domain
Infrastructure/  - External concerns, implements Application interfaces
Presentation/    - API/UI, depends on Application
```

### Error Handling

- Use the centralized error handling framework in `ErrorHandling/`
- Apply appropriate attributes: `[PointOfFailure]`, `[ErrorType]`, `[HandlerCode]`
- Create specific exception types for domain errors
- Include meaningful error messages

### Example Code

```csharp
public class ExampleService : IExampleService
{
    private readonly ILogger<ExampleService> _logger;

    public ExampleService(ILogger<ExampleService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [PointOfFailure(PointOfFailure.Service)]
    public async Task<Response<ResultDto>> ProcessAsync(RequestDto request)
    {
        _logger.LogInformation("Processing request for {Id}", request.Id);

        // Implementation

        return Response<ResultDto>.Success(result);
    }
}
```

## Commit Guidelines

### Commit Message Format

Follow the Conventional Commits specification:

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types

- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, no logic change)
- `refactor`: Code refactoring
- `test`: Adding or updating tests
- `chore`: Maintenance tasks, dependencies

### Examples

```
feat(agent): add memory thread keyword indexing

Implement keyword-based indexing for memory threads to improve
retrieval performance.

Closes #123
```

```
fix(api): resolve null reference in agent controller

Add null checks for agent configuration to prevent crashes
when configuration is missing.
```

## Pull Request Process

### Before Submitting

1. **Update your branch** with the latest from main:
   ```bash
   git fetch upstream
   git rebase upstream/main
   ```

2. **Run tests**:
   ```bash
   dotnet test
   ```

3. **Run build**:
   ```bash
   dotnet build
   ```

4. **Verify your changes** work as expected

### Submitting a PR

1. Push your branch to your fork:
   ```bash
   git push origin feat/your-feature-name
   ```

2. Open a Pull Request on GitHub

3. Fill out the PR template with:
   - Clear description of changes
   - Related issue numbers
   - Screenshots (if UI changes)
   - Testing performed

4. Wait for review and address feedback

### PR Requirements

- All tests must pass
- Code must build without warnings
- Follow coding standards
- Include tests for new functionality
- Update documentation if needed
- At least one approving review required

## Testing

### Writing Tests

- Place tests in the `tests/` directory
- Mirror the source structure
- Use descriptive test names: `MethodName_Scenario_ExpectedBehavior`
- Follow Arrange-Act-Assert pattern

### Example Test

```csharp
[Fact]
public async Task ProcessAsync_WithValidRequest_ReturnsSuccess()
{
    // Arrange
    var service = new ExampleService(_mockLogger.Object);
    var request = new RequestDto { Id = 1 };

    // Act
    var result = await service.ProcessAsync(request);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Data);
}
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/YourTestProject

# Run with coverage
dotnet test /p:CollectCoverage=true
```

## Feature Branches

Use the following branch naming conventions:

- `feat/issue-{number}-{description}` - New features
- `fix/issue-{number}-{description}` - Bug fixes
- `docs/{description}` - Documentation updates
- `refactor/{description}` - Code refactoring

## Questions?

If you have questions or need help:

1. Check existing documentation in `docs/`
2. Review closed issues and PRs
3. Open a new issue with the `question` label

## Recognition

Contributors will be recognized in the project documentation. Thank you for helping make Thalamus better!
