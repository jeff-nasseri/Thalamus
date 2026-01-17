/// <summary>
///     Firewall rule that validates request permissions.
///     Applies to all request types (commands and queries).
/// </summary>
[FirewallRuleCategory(FirewallRuleCategory.ALL)]
public class FirewallPermissionRule : IFirewallRule
{
    /// <inheritdoc />
    public bool IsAllowed<T>(T input) where T : FirewallRequestContext
    {
        throw new NotImplementedException();
    }
}