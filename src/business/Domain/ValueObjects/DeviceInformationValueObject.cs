using System.Runtime.InteropServices;
using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

/// <summary>
///     Represents device information including hardware capacity and operating system details.
///     This value object captures the capabilities of a device hosting agent nodes.
///     Uses .NET built-in serializable types for platform compatibility.
/// </summary>
public class DeviceInformationValueObject : ValueObject
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeviceInformationValueObject" /> class.
    /// </summary>
    public DeviceInformationValueObject()
    {
        OperatingSystem = string.Empty;
        Architecture = string.Empty;
        TotalMemoryMB = 0;
        ProcessorCount = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DeviceInformationValueObject" /> class with specified values.
    /// </summary>
    /// <param name="operatingSystem">The operating system description.</param>
    /// <param name="architecture">The processor architecture (e.g., X64, Arm64).</param>
    /// <param name="totalMemoryMB">Total memory in megabytes.</param>
    /// <param name="processorCount">Number of logical processors.</param>
    public DeviceInformationValueObject(string operatingSystem, string architecture, long totalMemoryMB,
        int processorCount)
    {
        OperatingSystem = operatingSystem ?? throw new ArgumentNullException(nameof(operatingSystem));
        Architecture = architecture ?? throw new ArgumentNullException(nameof(architecture));
        TotalMemoryMB = totalMemoryMB;
        ProcessorCount = processorCount;
        MachineName = Environment.MachineName;
        UserName = Environment.UserName;
        Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
        Is64BitProcess = Environment.Is64BitProcess;
    }

    /// <summary>
    ///     Gets or sets the operating system description.
    ///     Example: "Microsoft Windows NT 10.0.19045.0"
    /// </summary>
    public string OperatingSystem { get; set; }

    /// <summary>
    ///     Gets or sets the processor architecture.
    ///     Example: "X64", "Arm64", "X86"
    /// </summary>
    public string Architecture { get; set; }

    /// <summary>
    ///     Gets or sets the total physical memory in megabytes.
    /// </summary>
    public long TotalMemoryMB { get; set; }

    /// <summary>
    ///     Gets or sets the number of logical processors available to the system.
    /// </summary>
    public int ProcessorCount { get; set; }

    /// <summary>
    ///     Gets or sets the NetBIOS name of the local computer.
    /// </summary>
    public string? MachineName { get; set; }

    /// <summary>
    ///     Gets or sets the user name of the person who is currently logged on.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the current operating system is 64-bit.
    /// </summary>
    public bool Is64BitOperatingSystem { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the current process is 64-bit.
    /// </summary>
    public bool Is64BitProcess { get; set; }

    /// <summary>
    ///     Creates a DeviceInformationValueObject from the current system environment.
    /// </summary>
    /// <returns>A DeviceInformationValueObject populated with current system information.</returns>
    public static DeviceInformationValueObject FromCurrentEnvironment()
    {
        // Note: Getting actual physical memory requires platform-specific code
        // For cross-platform compatibility, we use processor count as a proxy
        var totalMemoryMB = Environment.ProcessorCount * 2048L; // Rough estimate: 2GB per core

        return new DeviceInformationValueObject(
            Environment.OSVersion.ToString(),
            RuntimeInformation.ProcessArchitecture.ToString(),
            totalMemoryMB,
            Environment.ProcessorCount
        );
    }

    /// <summary>
    ///     Gets the components that define the equality of the DeviceInformationValueObject.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OperatingSystem;
        yield return Architecture;
        yield return TotalMemoryMB;
        yield return ProcessorCount;
    }
}