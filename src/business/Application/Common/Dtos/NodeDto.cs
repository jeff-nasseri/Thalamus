namespace Application.Common.Dtos;

/// <summary>
///     Data transfer object for device information.
/// </summary>
/// <param name="OperatingSystem">The operating system description.</param>
/// <param name="Architecture">The processor architecture (e.g., X64, Arm64).</param>
/// <param name="TotalMemoryMB">Total memory in megabytes.</param>
/// <param name="ProcessorCount">Number of logical processors.</param>
/// <param name="MachineName">The NetBIOS name of the local computer.</param>
/// <param name="UserName">The user name of the person currently logged on.</param>
/// <param name="Is64BitOperatingSystem">Whether the current operating system is 64-bit.</param>
/// <param name="Is64BitProcess">Whether the current process is 64-bit.</param>
public record DeviceInformationDto(
    string OperatingSystem,
    string Architecture,
    long TotalMemoryMB,
    int ProcessorCount,
    string? MachineName = null,
    string? UserName = null,
    bool Is64BitOperatingSystem = false,
    bool Is64BitProcess = false
);

/// <summary>
///     Data transfer object for agent node information.
/// </summary>
/// <param name="RegisteredName">The registered name of this node in the system.</param>
/// <param name="DeviceInformation">Device information containing hardware and operating system details.</param>
/// <param name="Agents">The collection of agents hosted on this node.</param>
public record NodeDto(
    string RegisteredName,
    DeviceInformationDto? DeviceInformation = null,
    IEnumerable<AgentDto>? Agents = null
);