/// <summary>
///     Firewall rule that validates and prevents prompt injection attacks.
///     Applies to all request types (commands and queries).
/// </summary>
[FirewallRuleCategory(FirewallRuleCategory.ALL)]
public class FirewallPromptInjectionRule : IFirewallRule
{
    /// <inheritdoc />
    public bool IsAllowed<T>(T input) where T : FirewallRequestContext
    {
        throw new NotImplementedException();
    }
}