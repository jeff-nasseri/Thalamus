/// <summary>
///     Abstract base class for firewall messages.
///     Contains the cushion level for determining validation strictness.
/// </summary>
public abstract class BaseFirewallMessage
{
    /// <summary>
    ///     Gets the cushion level for this firewall message.
    /// </summary>
    protected FirewallCushion FirewallCushion { get; }
}