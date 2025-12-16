using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Exceptions;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services;

public class LeadService : ILeadService
{
    private readonly ILeadRepository _leadRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly ISalesTaskOrchestrator _salesTaskOrchestrator;
    private readonly IUnitOfWork _unitOfWork;

    public LeadService(
        ILeadRepository leadRepository,
        IServiceRepository serviceRepository,
        ISalesTaskOrchestrator salesTaskOrchestrator,
        IUnitOfWork unitOfWork)
    {
        _leadRepository = leadRepository;
        _serviceRepository = serviceRepository;
        _salesTaskOrchestrator = salesTaskOrchestrator;
        _unitOfWork = unitOfWork;
    }

    public async Task<LeadDto> CreateLeadAsync(CreateLeadDto createLeadDto)
    {
        var lead = new Lead
        {
            LeadId = Guid.NewGuid(),
            CompanyName = createLeadDto.CompanyName,
            ContactPerson = createLeadDto.ContactPerson,
            Email = createLeadDto.Email,
            PhoneNumber = createLeadDto.PhoneNumber,
            Status = "New",
            SalesStage = SalesStage.NEW,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _leadRepository.AddAsync(lead);
        await _unitOfWork.SaveChangesAsync();

        return new LeadDto
        {
            Id = lead.LeadId,
            CompanyName = lead.CompanyName,
            ContactPerson = lead.ContactPerson,
            Email = lead.Email,
            PhoneNumber = lead.PhoneNumber,
            Status = lead.Status
        };
    }

    public async Task<IEnumerable<LeadDto>> GetLeadsAsync()
    {
        var leads = await _leadRepository.GetAllAsync();
        return leads.Select(lead => new LeadDto
        {
            Id = lead.LeadId,
            CompanyName = lead.CompanyName,
            ContactPerson = lead.ContactPerson,
            Email = lead.Email,
            PhoneNumber = lead.PhoneNumber,
            Status = lead.Status
        });
    }

    public async Task<LeadDto?> GetLeadByIdAsync(Guid id)
    {
        var lead = await _leadRepository.GetByIdAsync(id);
        if (lead == null) return null;

        return new LeadDto
        {
            Id = lead.LeadId,
            CompanyName = lead.CompanyName,
            ContactPerson = lead.ContactPerson,
            Email = lead.Email,
            PhoneNumber = lead.PhoneNumber,
            Status = lead.Status
        };
    }

    public async Task UpdateLeadStageAsync(Guid id, UpdateLeadStageDto updateLeadStageDto)
    {
        var lead = await _leadRepository.GetByIdAsync(id);
        if (lead == null)
        {
            throw new NotFoundException(nameof(Lead), id);
        }

        if (!Enum.TryParse<SalesStage>(updateLeadStageDto.NewStage, true, out var newStage))
        {
            throw new ArgumentException($"Invalid sales stage: {updateLeadStageDto.NewStage}");
        }

        lead.SalesStage = newStage;
        lead.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AssignServiceToLeadAsync(Guid id, AssignServiceDto assignServiceDto)
    {
        var lead = await _leadRepository.GetByIdAsync(id);
        if (lead == null)
        {
            throw new NotFoundException(nameof(Lead), id);
        }

        var service = await _serviceRepository.GetByIdAsync(assignServiceDto.ServiceId);
        if (service == null)
        {
            throw new NotFoundException(nameof(Service), assignServiceDto.ServiceId);
        }

        // The key action is to create a new sales task for the initial outreach.
        await _salesTaskOrchestrator.CreateTaskAsync(id.ToString(), "InitialOutreach");

        lead.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();
    }
}
