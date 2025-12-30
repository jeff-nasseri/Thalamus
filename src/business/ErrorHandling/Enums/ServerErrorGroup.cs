using ErrorHandling.Attributes;

namespace ErrorHandling.Enums;

/// <summary>
/// Defines server error groups for categorizing errors by their source and responsibility.
/// </summary>
public enum ServerErrorGroup
{
    /// <summary>
    /// Indicates an application failure such as failure to communicate with internal services (e.g., database).
    /// </summary>
    [ErrorResponsibilityTeam(ErrorResponsibilityTeam.Development)]
    ApplicationFailure,

    /// <summary>
    /// Indicates a system failure such as issues with third-party packages or infrastructure.
    /// </summary>
    [ErrorResponsibilityTeam(ErrorResponsibilityTeam.SiteReliabilityEngineering)]
    [ErrorResponsibilityTeam(ErrorResponsibilityTeam.Development)]
    [ServerErrorGroup(ApplicationFailure)]
    SystemFailure,

    /// <summary>
    /// Indicates a security-related error requiring SRE attention.
    /// </summary>
    [ErrorResponsibilityTeam(ErrorResponsibilityTeam.SiteReliabilityEngineering)]
    Security,

    /// <summary>
    /// Indicates a business logic error (e.g., attempting to withdraw more money than available balance).
    /// </summary>
    [ErrorResponsibilityTeam(ErrorResponsibilityTeam.Development)]
    BusinessLogic,
}