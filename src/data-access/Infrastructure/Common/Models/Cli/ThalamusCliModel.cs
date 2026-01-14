namespace Infrastructure.Common.Models.Cli;

/// <summary>
/// Represents a model for CLI command structure and hierarchy.
/// Defines the properties of CLI commands including names, descriptions, and nested arguments.
/// </summary>
public class ThalamusCliModel
{
    /// <summary>
    /// Gets or sets the full name of the CLI command or argument.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the short name or alias for the CLI command or argument (e.g., "-m", "-i").
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// Gets or sets the description explaining the purpose of the command or argument.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the category that groups related commands or arguments.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the collection of nested arguments accepted by this command.
    /// Null if the command does not accept nested arguments.
    /// </summary>
    public IEnumerable<ThalamusCliModel>? AcceptedArgs { get; set; }

    /// <summary>
    /// Gets or sets the potential value or default value hint for the command or argument.
    /// Null if no value is expected or no default is specified.
    /// </summary>
    public string? PotentialValue { get; set; }
}