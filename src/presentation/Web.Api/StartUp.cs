using Application;
using AutoMapper.Internal;
using Serilog;
using Thalamus.Web.Extensions;
using Thalamus.Web.Middlewares;
using Thalamus.Web.Swagger.Extensions;

namespace Web.Api;

/// <summary>
///     Startup class responsible for configuring services and the application request pipeline.
/// </summary>
public class Startup
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Startup" /> class.
    /// </summary>
    /// <param name="configuration">Application configuration.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    ///     Gets the application configuration.
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    ///     Configures application services. This method is called by the runtime.
    /// </summary>
    /// <param name="services">Service collection to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.SetupControllers()
            .SetupDataProtection()
            .SetupCustomSwagger()
            .SetupSentryCustomisation()
            .SetupMediateR()
            .ConfigureSwaggerJwtAuthentication()
            .SetupKeycloak();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddAutoMapper(cfg => { cfg.Internal().MethodMappingEnabled = false; });

        services.AddApplication();
    }

    /// <summary>
    ///     Configures the HTTP request pipeline. This method is called by the runtime.
    /// </summary>
    /// <param name="app">Application builder to configure.</param>
    /// <param name="env">Web host environment information.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowOrigin");
        app.UseSerilogRequestLogging();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.SetupSwaggerUi();
        app.UseMiddleware<ErrorLoggingMiddleware>();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}