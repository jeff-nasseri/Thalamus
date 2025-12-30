using ErrorHandling.Attributes;

namespace ErrorHandling.Enums;

/// <summary>
/// Defines the teams responsible for handling different types of errors.
/// </summary>
public enum ErrorResponsibilityTeam
{
    /// <summary>
    /// Indicates the Site Reliability Engineering (SRE) team is responsible.
    /// </summary>
    [ErrorResponsibilityDetail("SRE Team", "TEAM_01")]
    SiteReliabilityEngineering,

    /// <summary>
    /// Indicates the Development team is responsible.
    /// </summary>
    [ErrorResponsibilityDetail("Development Team", "TEAM_02")]
    Development,
}