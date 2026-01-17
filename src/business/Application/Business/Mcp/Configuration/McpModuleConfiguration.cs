using Application.Common.Configuration;
using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Mcp.Configuration;

/// <summary>
///     AutoMapper configuration for MCP module.
///     Maps between McpPlugin domain entities and McpPluginDto data transfer objects.
/// </summary>
public class McpModuleConfiguration : Profile, IModuleConfiguration
{
    /// <summary>
    ///     Initializes the AutoMapper mappings for MCP module.
    /// </summary>
    public McpModuleConfiguration()
    {
        // Map from McpPluginDto to McpPlugin entity
        CreateMap<McpPluginDto, McpPlugin>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.McpConfigurationValueObject, opt => opt.MapFrom(src => src));

        // Map from McpPlugin entity to McpPluginDto
        CreateMap<McpPlugin, McpPluginDto>()
            .ForCtorParam("Title", opt => opt.MapFrom(src => src.Title))
            .ForCtorParam("Code", opt => opt.MapFrom(src => src.Code))
            .ForCtorParam("Description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("Enabled", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Enabled))
            .ForCtorParam("Platform", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Platform))
            .ForCtorParam("ImageName", opt => opt.MapFrom(src => src.McpConfigurationValueObject.ImageName))
            .ForCtorParam("ContainerName", opt => opt.MapFrom(src => src.McpConfigurationValueObject.ContainerName))
            .ForCtorParam("RestartPolicy", opt => opt.MapFrom(src => src.McpConfigurationValueObject.RestartPolicy))
            .ForCtorParam("Environment", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Environment))
            .ForCtorParam("Ports", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Ports))
            .ForCtorParam("Volumes", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Volumes))
            .ForCtorParam("Networks", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Networks))
            .ForCtorParam("Configuration", opt => opt.MapFrom(src => src.McpConfigurationValueObject.Configuration));

        // Map from McpPluginDto to McpConfigurationValueObject
        CreateMap<McpPluginDto, McpConfigurationValueObject>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.McpCode, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled))
            .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
            .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
            .ForMember(dest => dest.ContainerName, opt => opt.MapFrom(src => src.ContainerName))
            .ForMember(dest => dest.RestartPolicy, opt => opt.MapFrom(src => src.RestartPolicy))
            .ForMember(dest => dest.Environment, opt => opt.MapFrom(src => src.Environment))
            .ForMember(dest => dest.Ports, opt => opt.MapFrom(src => src.Ports))
            .ForMember(dest => dest.Volumes, opt => opt.MapFrom(src => src.Volumes))
            .ForMember(dest => dest.Networks, opt => opt.MapFrom(src => src.Networks))
            .ForMember(dest => dest.Configuration,
                opt => opt.MapFrom(src => src.Configuration ?? new Dictionary<string, object>()));
    }

    /// <inheritdoc />
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(McpModuleConfiguration));
        return services;
    }
}