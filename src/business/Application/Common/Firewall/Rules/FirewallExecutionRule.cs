/// <summary>
/// Firewall rule that validates execution permissions.
/// Applies to all request types (commands and queries).
/// </summary>
[FirewallRuleCategory(FirewallRuleCategory.ALL)]
public class FirewallExecutionRule : IFirewallRule
{
    /// <inheritdoc />
    public bool IsAllowed<T>(T input) where T : FirewallRequestContext
    {
        throw new NotImplementedException();
    }
}