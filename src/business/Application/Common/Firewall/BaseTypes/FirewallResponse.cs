using ErrorHandling;

/// <summary>
///     Generic firewall response containing a message of type <typeparamref name="T" />.
/// </summary>
/// <typeparam name="T">The type of firewall message, must inherit from <see cref="BaseFirewallMessage" />.</typeparam>
public class FirewallResponse<T> : Response<T> where T : BaseFirewallMessage
{
    protected FirewallResponse(Error error) : base(error)
    {
    }

    protected FirewallResponse(T? value) : base(value)
    {
    }
}