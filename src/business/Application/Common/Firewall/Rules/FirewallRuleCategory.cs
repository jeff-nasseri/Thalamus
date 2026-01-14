/// <summary>
/// Categorizes firewall rules by the type of operation they apply to.
/// </summary>
public enum FirewallRuleCategory
{
    /// <summary>
    /// Rules that apply only to command operations.
    /// </summary>
    COMMAND,

    /// <summary>
    /// Rules that apply only to query operations.
    /// </summary>
    QUERY,

    /// <summary>
    /// Rules that apply to all operations (both commands and queries).
    /// </summary>
    ALL
}