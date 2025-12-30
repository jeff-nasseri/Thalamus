namespace ErrorHandling.Attributes;

/// <summary>
/// Attribute to specify detailed responsibility information for error handling.
/// Applied to enum fields to document the team or component responsible for handling specific errors.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class ErrorResponsibilityDetailAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the title of the responsible party.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the responsibility.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the unique code identifying the responsible party.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponsibilityDetailAttribute"/> class.
    /// </summary>
    /// <param name="title">The title of the responsible party.</param>
    /// <param name="code">The unique code identifying the responsible party.</param>
    /// <param name="description">Optional description. Defaults to "{Title} is responsible for the raised error."</param>
    public ErrorResponsibilityDetailAttribute(string title, string code, string description = null!)
    {
        Title = title;
        Code = code;
        Description = description ?? $"{Title} is responsible for the raised error.";
    }
}