using AISalesAgent.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface IConfigurationService
{
    Task<IEnumerable<ServiceDto>> GetServicesAsync();
    Task<ServiceDto> GetServiceByIdAsync(Guid id);
    Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto);
    Task<IEnumerable<ServicePitchDto>> GetPitchesAsync();
    Task<ServicePitchDto> GetPitchByIdAsync(Guid id);
    Task<ServicePitchDto> CreatePitchAsync(CreateServicePitchDto createServicePitchDto);
    Task<IEnumerable<CtaDto>> GetCtasAsync();
    Task<CtaDto> GetCtaByIdAsync(Guid id);
    Task<CtaDto> CreateCtaAsync(CreateCtaDto createCtaDto);
    Task<AgentSettingsDto> GetAgentSettingsAsync();
    Task UpdateAgentSettingsAsync(UpdateAgentSettingsDto updateAgentSettingsDto);
}
