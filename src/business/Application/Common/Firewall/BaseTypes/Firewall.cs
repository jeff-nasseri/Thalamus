using ErrorHandling;

/// <summary>
///     Abstract base class for firewall implementations.
///     Manages firewall rules and validates requests based on configured rules.
/// </summary>
/// <typeparam name="T">The firewall response type.</typeparam>
/// <typeparam name="G">The firewall request context type.</typeparam>
/// <typeparam name="TMessage">The firewall message type.</typeparam>
public abstract class Firewall<T, G, TMessage>
    where T : FirewallResponse<TMessage>
    where G : FirewallRequestContext
    where TMessage : BaseFirewallMessage
{
    /// <summary>
    ///     Gets or sets the collection of firewall rules.
    /// </summary>
    protected IEnumerable<IFirewallRule> Rules { get; set; }

    /// <summary>
    ///     Registers an array of firewall rules to be used for validation.
    /// </summary>
    /// <param name="rules">The firewall rules to register.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected Task RegisterFirewallRulesAsync(IFirewallRule[] rules)
    {
        Rules = rules ?? throw new ArgumentNullException(nameof(rules));
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Validates the request context against configured firewall rules.
    /// </summary>
    /// <param name="context">The request context to validate.</param>
    /// <returns>A task containing the firewall response.</returns>
    protected abstract Task<T> ValidateAsync(G context);

    /// <summary>
    ///     Invokes a specific firewall rule for validation.
    /// </summary>
    /// <param name="rule">The firewall rule to invoke.</param>
    /// <returns>A task containing the validation response.</returns>
    protected abstract Task<Response> InvokeRuleAsync(IFirewallRule rule);
}