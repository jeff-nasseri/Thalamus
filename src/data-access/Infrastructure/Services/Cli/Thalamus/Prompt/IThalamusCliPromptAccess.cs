using ErrorHandling;
using Infrastructure.Services.Cli.Base;

namespace Infrastructure.Services.Cli.Thalamus.Prompt;

/// <summary>
/// Provides API access for the CLI to communicate with the agent using prompts.
/// Supports both synchronous (waiting) and asynchronous (background job) execution modes.
/// When running in background mode, the terminal returns immediately; otherwise, it waits for prompt completion.
/// </summary>
public interface IThalamusCliPromptAccess : ICliService
{
    /// <summary>
    /// Executes a prompt message to communicate with the agent.
    /// </summary>
    /// <param name="args">Command arguments including the message and optional background flag.</param>
    /// <returns>A response indicating the result of the prompt execution.</returns>
    Task<Response> ExecutePromptAsync(string[] args);
}