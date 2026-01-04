using Domain.Common.BaseTypes;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
///     Represents a physical or virtual node in the Thalamus distributed system.
///     An agent node hosts one or more agents and provides runtime environment and resources.
///     Contains information about the operating system, hardware resources (RAM, CPU), and hosted agents.
/// </summary>
public class AgentNode : BaseEntity
{
    /// <summary>
    ///     Gets or sets the registered name of this node in the system.
    /// </summary>
    public string RegisteredName { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the collection of agents hosted on this node.
    /// </summary>
    public IEnumerable<Agent>? Agents { get; set; }

    /// <summary>
    ///     Gets or sets the device information containing hardware and operating system details.
    ///     This property captures the capacity and capabilities of the device hosting this agent node.
    /// </summary>
    public DeviceInformationValueObject? DeviceInformation { get; set; }
}