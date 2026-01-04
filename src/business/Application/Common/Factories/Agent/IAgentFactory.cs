namespace Application.Common.Factories.Agent;

/// <summary>
///     Interface for agent factory implementations.
///     Defines the contract for factory tag identification.
/// </summary>
public interface IAgentFactory
{
    /// <summary>
    ///     Gets the unique tag that identifies this factory implementation.
    /// </summary>
    string Tag { get; }
}