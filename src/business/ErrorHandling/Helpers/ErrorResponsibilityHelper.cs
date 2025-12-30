using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace ErrorHandling.Helpers;

/// <summary>
/// Helper class for determining team responsibility for errors.
/// </summary>
public static class ErrorResponsibilityHelper
{
    /// <summary>
    /// Gets the responsible teams for a given server error group.
    /// Teams are determined by <see cref="ErrorResponsibilityTeamAttribute"/> annotations on the enum value.
    /// </summary>
    /// <param name="enum">The server error group enum value.</param>
    /// <returns>A collection of <see cref="ErrorResponsibilityTeam"/> values indicating responsible teams.</returns>
    public static IEnumerable<ErrorResponsibilityTeam> GetResponsibleTeams(ServerErrorGroup @enum)
    {
        IEnumerable<ErrorResponsibilityTeamAttribute> result =
            EnumHelper<ErrorResponsibilityTeamAttribute, ServerErrorGroup>.GetCustomAttributes(@enum);
        return result.Select(i => i.ErrorResponsibilityTeam);
    }
}