using ErrorHandling;

/// <summary>
///     Firewall response for Thalamus command operations.
/// </summary>
public class ThalamusCommandFirewallResponse : FirewallResponse<ThalamusCommandFirewallMessage>
{
    protected ThalamusCommandFirewallResponse(Error error) : base(error)
    {
    }

    protected ThalamusCommandFirewallResponse(ThalamusCommandFirewallMessage? value) : base(value)
    {
    }
}