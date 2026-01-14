/// <summary>
/// Attribute to specify the category of a firewall rule.
/// Can be applied multiple times to apply a rule to multiple categories.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class FirewallRuleCategoryAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the firewall rule category.
    /// </summary>
    public FirewallRuleCategory Category { get; set; }
}