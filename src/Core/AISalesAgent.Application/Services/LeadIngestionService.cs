using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services;

public class LeadIngestionService : ILeadIngestionService
{
    private readonly ILogger<LeadIngestionService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILeadRepository _leadRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly ISalesTaskOrchestrator _salesTaskOrchestrator;

    public LeadIngestionService(
        ILogger<LeadIngestionService> logger,
        IUnitOfWork unitOfWork,
        ILeadRepository leadRepository,
        IServiceRepository serviceRepository,
        ISalesTaskOrchestrator salesTaskOrchestrator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _leadRepository = leadRepository;
        _serviceRepository = serviceRepository;
        _salesTaskOrchestrator = salesTaskOrchestrator;
    }

    public async Task IngestLeadAsync(LeadInputDto input, LeadSource source, LeadChannel channel)
    {
        var normalizedEmail = input.Email?.ToLowerInvariant();
        var normalizedPhone = NormalizePhoneNumber(input.Phone);

        if (await IsDuplicateAsync(normalizedEmail, normalizedPhone))
        {
            _logger.LogWarning("Duplicate lead detected. Email: {Email}, Phone: {Phone}", normalizedEmail, normalizedPhone);
            return;
        }

        var lead = await CreateLeadEntity(input, source, channel, normalizedEmail, normalizedPhone);

        await _leadRepository.AddAsync(lead);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("New lead created with ID: {LeadId}", lead.LeadId);

        // Trigger the AI qualification and DISCOVERY stage
        await _salesTaskOrchestrator.CreateTaskAsync(lead.LeadId.ToString(), "StartDiscoveryProcess");

        _logger.LogInformation("Initial sales task created for lead ID: {LeadId}", lead.LeadId);
    }

    private string? NormalizePhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return null;
        }
        return Regex.Replace(phoneNumber, @"[^\d]", "");
    }

    private async Task<bool> IsDuplicateAsync(string? email, string? phone)
    {
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phone))
        {
            return false;
        }
        return await _leadRepository.ExistsByEmailOrPhoneAsync(email, phone);
    }

    private async Task<Lead> CreateLeadEntity(LeadInputDto input, LeadSource source, LeadChannel channel, string? email, string? phone)
    {
        var lead = new Lead
        {
            LeadId = Guid.NewGuid(),
            FirstName = input.Name, // Assuming full name for now
            Email = email,
            PhoneNumber = phone,
            CompanyName = input.Company,
            Source = $"{source}/{channel}", // Combine source and channel
            Status = "New",
            SalesStage = SalesStage.DISCOVERY, // Start at DISCOVERY
            AssignedTo = "AI",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (!string.IsNullOrWhiteSpace(input.ServiceInterest))
        {
            var matchedService = await _serviceRepository.FindByNameAsync(input.ServiceInterest);

            if (matchedService != null)
            {
                lead.ServiceId = matchedService.ServiceId;
            }
            else
            {
                _logger.LogWarning("Could not find a service matching interest: {ServiceInterest}", input.ServiceInterest);
            }
        }

        return lead;
    }
}
