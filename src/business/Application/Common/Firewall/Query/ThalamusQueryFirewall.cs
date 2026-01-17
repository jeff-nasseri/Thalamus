using ErrorHandling;

/// <summary>
///     Firewall implementation for Thalamus query operations.
///     Validates query requests against configured firewall rules.
/// </summary>
public class ThalamusQueryFirewall : Firewall<ThalamusQueryFirewallResponse, ThalamusQueryFirewallRequestContext,
    ThalamusQueryFirewallMessage>
{
    /// <inheritdoc />
    protected override Task<ThalamusQueryFirewallResponse> ValidateAsync(ThalamusQueryFirewallRequestContext context)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override Task<Response> InvokeRuleAsync(IFirewallRule rule)
    {
        throw new NotImplementedException();
    }
}