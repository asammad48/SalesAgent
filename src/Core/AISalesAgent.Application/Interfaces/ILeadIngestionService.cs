using AISalesAgent.Application.DTOs;
using AISalesAgent.Domain.Enums;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface ILeadIngestionService
{
    Task IngestLeadAsync(LeadInputDto input, LeadSource source, LeadChannel channel);
}
