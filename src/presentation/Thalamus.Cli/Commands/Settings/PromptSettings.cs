using System.ComponentModel;
using Spectre.Console.Cli;
using ValidationResult = Spectre.Console.ValidationResult;

namespace Thalamus.Cli.Commands.Settings;

/// <summary>
///     Settings for the prompt command.
/// </summary>
public class PromptSettings : CommandSettings
{
    [CommandOption("-m|--message")]
    [Description("The prompt message to execute (required)")]
    public string? Message { get; set; }

    [CommandOption("-b|--background")]
    [Description("Run prompt as background job. Empty value defaults to 'waiting' (synchronous execution)")]
    public bool Background { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Message)) return ValidationResult.Error("Message is required for prompt command");

        return ValidationResult.Success();
    }
}