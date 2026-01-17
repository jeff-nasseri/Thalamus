/// <summary>
///     Represents cushion levels for firewall protection.
///     Higher levels provide more stringent validation and protection.
/// </summary>
public enum FirewallCushion
{
    /// <summary>
    ///     No cushion - minimal validation.
    /// </summary>
    NONE = 0,

    /// <summary>
    ///     Low cushion level - basic validation.
    /// </summary>
    LOW = 1,

    /// <summary>
    ///     Medium cushion level - moderate validation.
    /// </summary>
    MEDIUM = 2,

    /// <summary>
    ///     High cushion level - strict validation.
    /// </summary>
    HIGH = 3,

    /// <summary>
    ///     Critical cushion level - maximum validation and protection.
    /// </summary>
    CRITICAL = 4
}