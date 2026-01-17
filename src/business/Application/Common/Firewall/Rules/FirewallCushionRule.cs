/// <summary>
///     Firewall rule that validates cushion level requirements.
///     Applies to all request types (commands and queries).
/// </summary>
[FirewallRuleCategory(FirewallRuleCategory.ALL)]
public class FirewallCushionRule : IFirewallRule
{
    /// <inheritdoc />
    public bool IsAllowed<T>(T input) where T : FirewallRequestContext
    {
        throw new NotImplementedException();
    }
}