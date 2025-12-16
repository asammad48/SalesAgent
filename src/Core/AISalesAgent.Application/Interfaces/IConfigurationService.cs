using AISalesAgent.Application.DTOs;

namespace AISalesAgent.Application.Interfaces;

public interface IConfigurationService
{
    Task<IEnumerable<ServiceDto>> GetServicesAsync();
    Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto);
    Task<IEnumerable<ServicePitchDto>> GetPitchesAsync();
    Task<ServicePitchDto> CreatePitchAsync(CreateServicePitchDto createServicePitchDto);
    Task<IEnumerable<CtaDto>> GetCtasAsync();
    Task<CtaDto> CreateCtaAsync(CreateCtaDto createCtaDto);
    Task<AgentSettingsDto> GetAgentSettingsAsync();
    Task UpdateAgentSettingsAsync(UpdateAgentSettingsDto updateAgentSettingsDto);
}
