using ErrorHandling;

/// <summary>
/// Generic firewall response containing a message of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of firewall message, must inherit from <see cref="BaseFirewallMessage"/>.</typeparam>
public class FirewallResponse<T> : Response<T> where T : BaseFirewallMessage
{
}