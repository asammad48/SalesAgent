using AISalesAgent.Application.DTOs;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface ILeadAdapters
{
    Task ProcessWebsiteLead(LeadInputDto lead);
    Task ProcessManualLead(LeadInputDto lead);
    Task ProcessLinkedInLead(LeadInputDto lead);
    Task ProcessFacebookLead(LeadInputDto lead);
    Task ProcessInstagramLead(LeadInputDto lead);
    Task ProcessGoogleLead(LeadInputDto lead);
}
