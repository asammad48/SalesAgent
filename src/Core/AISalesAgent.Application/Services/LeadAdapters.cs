using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Enums;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services;

public class LeadAdapters : ILeadAdapters
{
    private readonly ILeadIngestionService _leadIngestionService;

    public LeadAdapters(ILeadIngestionService leadIngestionService)
    {
        _leadIngestionService = leadIngestionService;
    }

    public Task ProcessWebsiteLead(LeadInputDto lead)
    {
        // Assuming Chat and Form as possible channels for a website lead
        var isChat = "Chat".Equals(lead.AdditionalFields?.GetValueOrDefault("Channel"), StringComparison.OrdinalIgnoreCase);
        return _leadIngestionService.IngestLeadAsync(lead, LeadSource.Website, isChat ? LeadChannel.Chat : LeadChannel.Form);
    }

    public Task ProcessManualLead(LeadInputDto lead)
    {
        return _leadIngestionService.IngestLeadAsync(lead, LeadSource.Manual, LeadChannel.Form);
    }

    public Task ProcessLinkedInLead(LeadInputDto lead)
    {
        return _leadIngestionService.IngestLeadAsync(lead, LeadSource.LinkedIn, LeadChannel.Form);
    }

    public Task ProcessFacebookLead(LeadInputDto lead)
    {
        return _leadIngestionService.IngestLeadAsync(lead, LeadSource.Facebook, LeadChannel.Form);
    }

    public Task ProcessInstagramLead(LeadInputDto lead)
    {
        return _leadIngestionService.IngestLeadAsync(lead, LeadSource.Instagram, LeadChannel.DM);
    }

    public Task ProcessGoogleLead(LeadInputDto lead)
    {
        return _leadIngestionService.IngestLeadAsync(lead, LeadSource.Google, LeadChannel.Form);
    }
}
