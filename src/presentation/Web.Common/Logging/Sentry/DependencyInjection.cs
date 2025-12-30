using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sentry;
using Sentry.Extensibility;
using Sentry.Serilog;
using Serilog;
using Serilog.Events;

namespace Thalamus.Web.Logging.Sentry;

/// <summary>
/// Provides dependency injection extensions for Serilog and Sentry integration.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures Serilog with Sentry integration using environment-based settings.
    /// For this integration you need to setup following parameters in your .env file: SENTRY_DSN and SENTRY_ENVIRONMENT.
    /// Please add HTTP Context Accessor to your code before using this integration.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <returns>The configured host builder.</returns>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpservicecollectionextensions.addhttpcontextaccessor?view=aspnetcore-7.0"/>
    public static IHostBuilder SetupSerilogWithSentry(this IHostBuilder builder)
    {
        IHostBuilder? result = builder.UseSerilog((context, serviceProvider, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(serviceProvider);

            SentrySetting setting = context.Configuration.Get<SentrySetting>()!;

            if (setting.IsActive)
            {
                configuration.WriteTo.Sentry(options =>
                {
                    options.Dsn = setting.Dsn;
                    options.MinimumEventLevel = LogEventLevel.Warning;
                    options.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                    options.ReportAssembliesMode = ReportAssembliesMode.None;
                    options.Debug = false;
                    options.AddEventProcessorProvider(() => new List<ISentryEventProcessor>
                    {
                        serviceProvider.GetRequiredService<CustomSentryEventProcessor>()
                    });
                });
            }
        });

        return result;
    }

    /// <summary>
    /// Configures Serilog with Sentry integration using custom configuration options.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="configureOptions">Action to configure Sentry Serilog options.</param>
    /// <returns>The configured host builder.</returns>
    public static IHostBuilder SetupSerilogWithSentry(this IHostBuilder builder, Action<SentrySerilogOptions> configureOptions)
    {
        IHostBuilder? result = builder.UseSerilog((context, serviceProvider, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(serviceProvider);

            SentrySetting setting = context.Configuration.Get<SentrySetting>()!;

            if (setting.IsActive)
            {
                configuration.WriteTo.Sentry(configureOptions);
            }
        });
        return result;
    }
}