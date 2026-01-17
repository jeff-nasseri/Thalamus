using ErrorHandling.Enums;

namespace ErrorHandling.Attributes;

/// <summary>
///     Attribute to assign team responsibility for error handling.
///     Can be applied multiple times to assign responsibility to multiple teams.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ErrorResponsibilityTeamAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ErrorResponsibilityTeamAttribute" /> class.
    /// </summary>
    /// <param name="team">The team responsible for handling the error.</param>
    public ErrorResponsibilityTeamAttribute(ErrorResponsibilityTeam team)
    {
        ErrorResponsibilityTeam = team;
    }

    /// <summary>
    ///     Gets or sets the team responsible for handling the error.
    /// </summary>
    public ErrorResponsibilityTeam ErrorResponsibilityTeam { get; set; }
}