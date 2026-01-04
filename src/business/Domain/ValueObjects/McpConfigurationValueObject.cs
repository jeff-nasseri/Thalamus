using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

/// <summary>
///     Represents the configuration for a Model Context Protocol (MCP) plugin.
///     Contains metadata and runtime configuration for MCP execution through various providers (Docker, etc.).
///     Based on the mcp-pool.json configuration schema.
/// </summary>
public class McpConfigurationValueObject : ValueObject, IValueObjectParser<McpConfigurationValueObject, string>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="McpConfigurationValueObject" /> class.
    /// </summary>
    public McpConfigurationValueObject()
    {
        Title = string.Empty;
        McpCode = string.Empty;
        Description = string.Empty;
        Platform = string.Empty;
        Configuration = new Dictionary<string, object>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="McpConfigurationValueObject" /> class with specified values.
    /// </summary>
    /// <param name="title">The display title of the MCP.</param>
    /// <param name="mcpCode">The unique code identifier for the MCP.</param>
    /// <param name="description">The description of MCP functionality.</param>
    /// <param name="platform">The runtime platform (e.g., "docker", "process", "kubernetes").</param>
    /// <param name="enabled">Whether the MCP is enabled for use.</param>
    public McpConfigurationValueObject(string title, string mcpCode, string description, string platform,
        bool enabled = true)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        McpCode = mcpCode ?? throw new ArgumentNullException(nameof(mcpCode));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Platform = platform ?? throw new ArgumentNullException(nameof(platform));
        Enabled = enabled;
        Configuration = new Dictionary<string, object>();
    }

    /// <summary>
    ///     Gets or sets the display title of the MCP.
    ///     Example: "IO MCP", "MikroTik Network MCP"
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Gets or sets the unique code identifier for this MCP.
    ///     Example: "io-mcp-v1", "mikrotik-mcp-v1"
    /// </summary>
    public string McpCode { get; set; }

    /// <summary>
    ///     Gets or sets the description of the MCP's functionality and purpose.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Gets or sets whether this MCP is enabled for use.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    ///     Gets or sets the runtime platform for executing the MCP.
    ///     Supported values: "docker", "process", "kubernetes"
    /// </summary>
    public string Platform { get; set; }

    /// <summary>
    ///     Gets or sets the platform-specific configuration for running the MCP.
    ///     For Docker: contains image, container_name, volumes, environment, ports, networks, health_check
    ///     For Process: contains executable path, arguments, working directory
    ///     For Kubernetes: contains deployment, service, configmap specifications
    /// </summary>
    public Dictionary<string, object> Configuration { get; set; }

    /// <summary>
    ///     Gets or sets the Docker image name (when Platform is "docker").
    ///     Example: "thalamus/io-mcp:latest"
    /// </summary>
    public string? ImageName { get; set; }

    /// <summary>
    ///     Gets or sets the container name (when Platform is "docker").
    ///     Example: "io-mcp-production"
    /// </summary>
    public string? ContainerName { get; set; }

    /// <summary>
    ///     Gets or sets the restart policy for the MCP.
    ///     Example: "unless-stopped", "always", "on-failure"
    /// </summary>
    public string? RestartPolicy { get; set; }

    /// <summary>
    ///     Gets or sets the environment variables for the MCP runtime.
    /// </summary>
    public Dictionary<string, string>? Environment { get; set; }

    /// <summary>
    ///     Gets or sets the port mappings for the MCP (host:container format).
    ///     Example: ["8080:8080", "8443:443"]
    /// </summary>
    public List<string>? Ports { get; set; }

    /// <summary>
    ///     Gets or sets the volume mappings for the MCP (host:container format).
    ///     Example: ["/data:/app/data", "/logs:/app/logs"]
    /// </summary>
    public List<string>? Volumes { get; set; }

    /// <summary>
    ///     Gets or sets the network names to connect the MCP to.
    ///     Example: ["thalamus-network"]
    /// </summary>
    public List<string>? Networks { get; set; }

    /// <summary>
    ///     Attempts to parse a JSON string into a McpConfigurationValueObject.
    /// </summary>
    /// <param name="input">The JSON string to parse.</param>
    /// <param name="valueObject">The parsed McpConfigurationValueObject if successful; otherwise, null.</param>
    /// <returns>True if parsing was successful; otherwise, false.</returns>
    public static bool TryParse(string input, out McpConfigurationValueObject? valueObject)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                valueObject = null;
                return false;
            }

            // Basic parsing - in production, use System.Text.Json or Newtonsoft.Json
            // This is a placeholder implementation
            valueObject = new McpConfigurationValueObject();
            return true;
        }
        catch
        {
            valueObject = null;
            return false;
        }
    }

    /// <summary>
    ///     Parses a JSON string into a McpConfigurationValueObject.
    /// </summary>
    /// <param name="input">The JSON string to parse.</param>
    /// <returns>The parsed McpConfigurationValueObject.</returns>
    /// <exception cref="ArgumentException">Thrown when the input cannot be parsed.</exception>
    public static McpConfigurationValueObject Parse(string input)
    {
        if (!TryParse(input, out var valueObject) || valueObject == null)
            throw new ArgumentException($"Cannot parse '{input}' into a McpConfigurationValueObject.", nameof(input));

        return valueObject;
    }

    /// <summary>
    ///     Sets the Docker-specific configuration.
    /// </summary>
    /// <param name="imageName">The Docker image name.</param>
    /// <param name="containerName">The container name.</param>
    /// <param name="restartPolicy">The restart policy.</param>
    public void SetDockerConfiguration(string imageName, string containerName, string restartPolicy = "unless-stopped")
    {
        ImageName = imageName ?? throw new ArgumentNullException(nameof(imageName));
        ContainerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
        RestartPolicy = restartPolicy;
        Platform = "docker";
    }

    /// <summary>
    ///     Adds an environment variable to the MCP configuration.
    /// </summary>
    /// <param name="key">The environment variable name.</param>
    /// <param name="value">The environment variable value.</param>
    public void AddEnvironmentVariable(string key, string value)
    {
        Environment ??= new Dictionary<string, string>();
        Environment[key] = value;
    }

    /// <summary>
    ///     Adds a port mapping to the MCP configuration.
    /// </summary>
    /// <param name="portMapping">The port mapping in "host:container" format.</param>
    public void AddPortMapping(string portMapping)
    {
        Ports ??= new List<string>();
        Ports.Add(portMapping);
    }

    /// <summary>
    ///     Adds a volume mapping to the MCP configuration.
    /// </summary>
    /// <param name="volumeMapping">The volume mapping in "host:container" format.</param>
    public void AddVolumeMapping(string volumeMapping)
    {
        Volumes ??= new List<string>();
        Volumes.Add(volumeMapping);
    }

    /// <summary>
    ///     Gets the components that define the equality of the McpConfigurationValueObject.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return McpCode;
        yield return Platform;
        yield return Title;
    }
}