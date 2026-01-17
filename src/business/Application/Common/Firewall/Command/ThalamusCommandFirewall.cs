using ErrorHandling;

/// <summary>
///     Firewall implementation for Thalamus command operations.
///     Validates command requests against configured firewall rules.
/// </summary>
public class ThalamusCommandFirewall : Firewall<ThalamusCommandFirewallResponse, ThalamusCommandFirewallRequestContext,
    ThalamusCommandFirewallMessage>
{
    /// <inheritdoc />
    protected override Task<ThalamusCommandFirewallResponse> ValidateAsync(
        ThalamusCommandFirewallRequestContext context)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override Task<Response> InvokeRuleAsync(IFirewallRule rule)
    {
        throw new NotImplementedException();
    }
}