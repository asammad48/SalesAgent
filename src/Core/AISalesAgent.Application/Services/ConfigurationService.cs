using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Exceptions;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IServicePitchRepository _pitchRepository;
    private readonly ICTARepository _ctaRepository;
    private readonly IAgentSettingsRepository _agentSettingsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfigurationService(
        IServiceRepository serviceRepository,
        IServicePitchRepository pitchRepository,
        ICTARepository ctaRepository,
        IAgentSettingsRepository agentSettingsRepository,
        IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _pitchRepository = pitchRepository;
        _ctaRepository = ctaRepository;
        _agentSettingsRepository = agentSettingsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ServiceDto>> GetServicesAsync()
    {
        var services = await _serviceRepository.GetAllAsync();
        return services.Select(s => new ServiceDto { ServiceId = s.ServiceId, ServiceName = s.ServiceName, ServiceDescription = s.ServiceDescription, IsActive = s.IsActive });
    }

    public async Task<ServiceDto> GetServiceByIdAsync(Guid id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null)
        {
            throw new NotFoundException(nameof(Service), id);
        }
        return new ServiceDto { ServiceId = service.ServiceId, ServiceName = service.ServiceName, ServiceDescription = service.ServiceDescription, IsActive = service.IsActive };
    }

    public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto)
    {
        var service = new Service
        {
            ServiceId = Guid.NewGuid(),
            ServiceName = createServiceDto.ServiceName,
            ServiceDescription = createServiceDto.ServiceDescription,
            IsActive = createServiceDto.IsActive
        };
        await _serviceRepository.AddAsync(service);
        await _unitOfWork.SaveChangesAsync();
        return new ServiceDto { ServiceId = service.ServiceId, ServiceName = service.ServiceName, ServiceDescription = service.ServiceDescription, IsActive = service.IsActive };
    }

    public async Task<IEnumerable<ServicePitchDto>> GetPitchesAsync()
    {
        var pitches = await _pitchRepository.GetAllAsync();
        return pitches.Select(p => new ServicePitchDto { PitchId = p.PitchId, ServiceId = p.ServiceId, PitchType = p.PitchType, PitchTitle = p.PitchTitle, PitchText = p.PitchText });
    }

    public async Task<ServicePitchDto> GetPitchByIdAsync(Guid id)
    {
        var pitch = await _pitchRepository.GetByIdAsync(id);
        if (pitch == null)
        {
            throw new NotFoundException(nameof(ServicePitch), id);
        }
        return new ServicePitchDto { PitchId = pitch.PitchId, ServiceId = pitch.ServiceId, PitchType = pitch.PitchType, PitchTitle = pitch.PitchTitle, PitchText = pitch.PitchText };
    }

    public async Task<ServicePitchDto> CreatePitchAsync(CreateServicePitchDto createServicePitchDto)
    {
        var pitch = new ServicePitch
        {
            PitchId = Guid.NewGuid(),
            ServiceId = createServicePitchDto.ServiceId,
            PitchType = createServicePitchDto.PitchType,
            PitchTitle = createServicePitchDto.PitchTitle,
            PitchText = createServicePitchDto.PitchText
        };
        await _pitchRepository.AddAsync(pitch);
        await _unitOfWork.SaveChangesAsync();
        return new ServicePitchDto { PitchId = pitch.PitchId, ServiceId = pitch.ServiceId, PitchType = pitch.PitchType, PitchTitle = pitch.PitchTitle, PitchText = pitch.PitchText };
    }

    public async Task<IEnumerable<CtaDto>> GetCtasAsync()
    {
        var ctas = await _ctaRepository.GetAllAsync();
        return ctas.Select(c => new CtaDto { CTAId = c.CTAId, CTAText = c.CTAText, CTAType = c.CTAType, IsActive = c.IsActive });
    }

    public async Task<CtaDto> GetCtaByIdAsync(Guid id)
    {
        var cta = await _ctaRepository.GetByIdAsync(id);
        if (cta == null)
        {
            throw new NotFoundException(nameof(CTA), id);
        }
        return new CtaDto { CTAId = cta.CTAId, CTAText = cta.CTAText, CTAType = cta.CTAType, IsActive = c.IsActive };
    }

    public async Task<CtaDto> CreateCtaAsync(CreateCtaDto createCtaDto)
    {
        var cta = new CTA
        {
            CTAId = Guid.NewGuid(),
            CTAText = createCtaDto.CTAText,
            CTAType = createCtaDto.CTAType,
            IsActive = createCtaDto.IsActive
        };
        await _ctaRepository.AddAsync(cta);
        await _unitOfWork.SaveChangesAsync();
        return new CtaDto { CTAId = cta.CTAId, CTAText = cta.CTAText, CTAType = cta.CTAType, IsActive = cta.IsActive };
    }

    public async Task<AgentSettingsDto> GetAgentSettingsAsync()
    {
        var settings = (await _agentSettingsRepository.GetAllAsync()).FirstOrDefault();
        if (settings == null)
        {
            settings = new AgentSettings();
            await _agentSettingsRepository.AddAsync(settings);
            await _unitOfWork.SaveChangesAsync();
        }
        return new AgentSettingsDto { AgentId = settings.AgentId, AgentName = settings.AgentName, AgentPersona = settings.AgentPersona, DefaultTone = settings.DefaultTone, DefaultLanguage = settings.DefaultLanguage, TimeZone = settings.TimeZone };
    }

    public async Task UpdateAgentSettingsAsync(UpdateAgentSettingsDto updateAgentSettingsDto)
    {
        var settings = (await _agentSettingsRepository.GetAllAsync()).FirstOrDefault();
        if (settings == null)
        {
            throw new NotFoundException(nameof(AgentSettings), "default");
        }

        settings.AgentName = updateAgentSettingsDto.AgentName;
        settings.AgentPersona = updateAgentSettingsDto.AgentPersona;
        settings.DefaultTone = updateAgentSettingsDto.DefaultTone;
        settings.DefaultLanguage = updateAgentSettingsDto.DefaultLanguage;
        settings.TimeZone = updateAgentSettingsDto.TimeZone;
        settings.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();
    }
}
