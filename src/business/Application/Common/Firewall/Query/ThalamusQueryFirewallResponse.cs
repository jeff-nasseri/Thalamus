using ErrorHandling;

/// <summary>
///     Firewall response for Thalamus query operations.
/// </summary>
public class ThalamusQueryFirewallResponse : FirewallResponse<ThalamusQueryFirewallMessage>
{
    protected ThalamusQueryFirewallResponse(Error error) : base(error)
    {
    }

    protected ThalamusQueryFirewallResponse(ThalamusQueryFirewallMessage? value) : base(value)
    {
    }
}