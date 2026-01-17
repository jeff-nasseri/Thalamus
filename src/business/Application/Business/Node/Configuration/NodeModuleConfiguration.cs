using Application.Common.Configuration;
using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Node.Configuration;

/// <summary>
///     AutoMapper configuration for Node module.
///     Maps between AgentNode domain entities and NodeDto data transfer objects.
/// </summary>
public class NodeModuleConfiguration : Profile, IModuleConfiguration
{
    /// <summary>
    ///     Initializes the AutoMapper mappings for Node module.
    /// </summary>
    public NodeModuleConfiguration()
    {
        // Map from NodeDto to AgentNode entity
        CreateMap<NodeDto, AgentNode>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RegisteredName, opt => opt.MapFrom(src => src.RegisteredName))
            .ForMember(dest => dest.DeviceInformation, opt => opt.MapFrom(src => src.DeviceInformation))
            .ForMember(dest => dest.Agents, opt => opt.MapFrom(src => src.Agents));

        // Map from AgentNode entity to NodeDto
        CreateMap<AgentNode, NodeDto>()
            .ForCtorParam("RegisteredName", opt => opt.MapFrom(src => src.RegisteredName))
            .ForCtorParam("DeviceInformation", opt => opt.MapFrom(src => src.DeviceInformation))
            .ForCtorParam("Agents", opt => opt.MapFrom(src => src.Agents));

        // Map from DeviceInformationDto to DeviceInformationValueObject
        CreateMap<DeviceInformationDto, DeviceInformationValueObject>()
            .ForMember(dest => dest.OperatingSystem, opt => opt.MapFrom(src => src.OperatingSystem))
            .ForMember(dest => dest.Architecture, opt => opt.MapFrom(src => src.Architecture))
            .ForMember(dest => dest.TotalMemoryMB, opt => opt.MapFrom(src => src.TotalMemoryMB))
            .ForMember(dest => dest.ProcessorCount, opt => opt.MapFrom(src => src.ProcessorCount))
            .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.MachineName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Is64BitOperatingSystem, opt => opt.MapFrom(src => src.Is64BitOperatingSystem))
            .ForMember(dest => dest.Is64BitProcess, opt => opt.MapFrom(src => src.Is64BitProcess));

        // Map from DeviceInformationValueObject to DeviceInformationDto
        CreateMap<DeviceInformationValueObject, DeviceInformationDto>()
            .ForCtorParam("OperatingSystem", opt => opt.MapFrom(src => src.OperatingSystem))
            .ForCtorParam("Architecture", opt => opt.MapFrom(src => src.Architecture))
            .ForCtorParam("TotalMemoryMB", opt => opt.MapFrom(src => src.TotalMemoryMB))
            .ForCtorParam("ProcessorCount", opt => opt.MapFrom(src => src.ProcessorCount))
            .ForCtorParam("MachineName", opt => opt.MapFrom(src => src.MachineName))
            .ForCtorParam("UserName", opt => opt.MapFrom(src => src.UserName))
            .ForCtorParam("Is64BitOperatingSystem", opt => opt.MapFrom(src => src.Is64BitOperatingSystem))
            .ForCtorParam("Is64BitProcess", opt => opt.MapFrom(src => src.Is64BitProcess));
    }

    /// <inheritdoc />
    public IServiceCollection RegisterConfiguration(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(NodeModuleConfiguration));
        return services;
    }
}