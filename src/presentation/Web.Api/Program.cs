using Thalamus.Web.Logging.Sentry;
using Infrastructure.Common;
using Host = Microsoft.Extensions.Hosting.Host;

namespace Web.Api;

/// <summary>
/// Entry point class for the Web API application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point of the application. Sets up the environment and runs the web host.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static async Task Main(string[] args)
    {
        EnvUtils.SetupEnvFile();

        IHost host = CreateNewHostBuilder(args).Build();

        await host.RunAsync();
    }

    /// <summary>
    /// Creates and configures the host builder with default settings, Startup configuration, and Serilog with Sentry integration.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <returns>Configured host builder instance.</returns>
    private static IHostBuilder CreateNewHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .SetupSerilogWithSentry();
    }
}