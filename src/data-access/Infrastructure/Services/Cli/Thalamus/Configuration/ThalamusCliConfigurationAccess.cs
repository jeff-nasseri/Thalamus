using System.Text.Json;
using Application.Business.Agent.Commands.InitializeAgent;
using Application.Business.Mcp.Commands.InitializeMcp;
using Application.Business.Node.Commands.InitializeNode;
using Application.Common.Dtos;
using ErrorHandling;
using MediatR;

namespace Infrastructure.Services.Cli.Thalamus.Configuration;

/// <summary>
///     Implementation of configuration access services for the Thalamus CLI.
///     Handles configuration initialization and dashboard setup operations.
/// </summary>
public class ThalamusCliConfigurationAccess : IThalamusCliConfigurationAccess
{
    private readonly string _baseConfigurationPath;
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ThalamusCliConfigurationAccess" /> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator for sending commands.</param>
    /// <param name="baseConfigurationPath">The base path where configuration JSON files are located.</param>
    public ThalamusCliConfigurationAccess(IMediator mediator, string baseConfigurationPath = "")
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _baseConfigurationPath = string.IsNullOrEmpty(baseConfigurationPath)
            ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
            : baseConfigurationPath;
    }

    /// <inheritdoc />
    public Task<Response> ExecuteAsync(string[] args)
    {
        return IdempotentConfigurationInitializationAsync(args);
    }

    /// <inheritdoc />
    public async Task<Response> IdempotentConfigurationInitializationAsync(string[] args)
    {
        try
        {
            var results = new List<string>();

            // Initialize Agents if requested
            if (args.Contains("agent") || args.Contains("all"))
            {
                var agentResult = await InitializeAgentsAsync();
                results.Add($"Agents: {(agentResult.Success ? "Success" : "Failed")}");
            }

            // Initialize MCP Plugins if requested
            if (args.Contains("mcp") || args.Contains("all"))
            {
                var mcpResult = await InitializeMcpPluginsAsync();
                results.Add($"MCP Plugins: {(mcpResult.Success ? "Success" : "Failed")}");
            }

            // Initialize Nodes if requested
            if (args.Contains("node") || args.Contains("all"))
            {
                var nodeResult = await InitializeNodesAsync();
                results.Add($"Nodes: {(nodeResult.Success ? "Success" : "Failed")}");
            }

            if (results.Count == 0) return ThalamusCliConfigurationErrorCodes.EmptyConfiguration;

            return Response.Successful();
        }
        catch (Exception)
        {
            return ThalamusCliConfigurationErrorCodes.UnknownInitializationError;
        }
    }

    /// <inheritdoc />
    public Task<Response> SetupWebDashboardAsync(string[] args)
    {
        throw new NotImplementedException();
    }

    private async Task<Response> InitializeAgentsAsync()
    {
        try
        {
            var filePath = Path.Combine(_baseConfigurationPath, "agent-pool.json");
            if (!File.Exists(filePath)) return ThalamusCliConfigurationErrorCodes.ConfigurationFileNotFound;

            var jsonContent = await File.ReadAllTextAsync(filePath);
            var agentData = JsonSerializer.Deserialize<AgentPoolConfiguration>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (agentData?.Agents == null || !agentData.Agents.Any())
                return ThalamusCliConfigurationErrorCodes.EmptyConfiguration;

            var request = new InitializeAgentsRequest(new InitializeAgentsRequestDto(agentData.Agents));
            var result = await _mediator.Send(request);

            return result.Success
                ? Response.Successful()
                : result.Error!.Value;
        }
        catch (Exception)
        {
            return ThalamusCliConfigurationErrorCodes.UnknownInitializationError;
        }
    }

    private async Task<Response> InitializeMcpPluginsAsync()
    {
        try
        {
            var filePath = Path.Combine(_baseConfigurationPath, "mcp-pool.json");
            if (!File.Exists(filePath)) return ThalamusCliConfigurationErrorCodes.ConfigurationFileNotFound;

            var jsonContent = await File.ReadAllTextAsync(filePath);
            var mcpData = JsonSerializer.Deserialize<McpPoolConfiguration>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (mcpData?.Plugins == null || !mcpData.Plugins.Any())
                return ThalamusCliConfigurationErrorCodes.EmptyConfiguration;

            var request = new InitializeMcpRequest(new InitializeMcpRequestDto(mcpData.Plugins));
            var result = await _mediator.Send(request);

            return result.Success
                ? Response.Successful()
                : result.Error!.Value;
        }
        catch (Exception)
        {
            return ThalamusCliConfigurationErrorCodes.UnknownInitializationError;
        }
    }

    private async Task<Response> InitializeNodesAsync()
    {
        try
        {
            var filePath = Path.Combine(_baseConfigurationPath, "node-pool.json");
            if (!File.Exists(filePath)) return ThalamusCliConfigurationErrorCodes.ConfigurationFileNotFound;

            var jsonContent = await File.ReadAllTextAsync(filePath);
            var nodeData = JsonSerializer.Deserialize<NodePoolConfiguration>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (nodeData?.Nodes == null || !nodeData.Nodes.Any())
                return ThalamusCliConfigurationErrorCodes.EmptyConfiguration;

            var request = new InitializeNodeRequest(new InitializeNodeRequestDto(nodeData.Nodes));
            var result = await _mediator.Send(request);

            return result.Success
                ? Response.Successful()
                : result.Error!.Value;
        }
        catch (Exception)
        {
            return ThalamusCliConfigurationErrorCodes.UnknownInitializationError;
        }
    }

    /// <summary>
    ///     Internal class for deserializing agent-pool.json
    /// </summary>
    private class AgentPoolConfiguration
    {
        public IEnumerable<AgentDto> Agents { get; set; } = Enumerable.Empty<AgentDto>();
    }

    /// <summary>
    ///     Internal class for deserializing mcp-pool.json
    /// </summary>
    private class McpPoolConfiguration
    {
        public IEnumerable<McpPluginDto> Plugins { get; set; } = Enumerable.Empty<McpPluginDto>();
    }

    /// <summary>
    ///     Internal class for deserializing node-pool.json
    /// </summary>
    private class NodePoolConfiguration
    {
        public IEnumerable<NodeDto> Nodes { get; set; } = Enumerable.Empty<NodeDto>();
    }
}