/// <summary>
///     Interface for firewall rules that validate requests.
/// </summary>
public interface IFirewallRule
{
    /// <summary>
    ///     Determines whether the request is allowed based on the rule.
    /// </summary>
    /// <typeparam name="T">The type of firewall request context.</typeparam>
    /// <param name="input">The request context to validate.</param>
    /// <returns>True if the request is allowed; otherwise, false.</returns>
    bool IsAllowed<T>(T input) where T : FirewallRequestContext;
}