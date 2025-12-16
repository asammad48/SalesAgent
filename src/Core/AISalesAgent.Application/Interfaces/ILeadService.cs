using AISalesAgent.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface ILeadService
{
    Task<LeadDto> CreateLeadAsync(CreateLeadDto createLeadDto);
    Task<IEnumerable<LeadDto>> GetLeadsAsync();
    Task<LeadDto> GetLeadByIdAsync(Guid id);
    Task UpdateLeadStageAsync(Guid id, UpdateLeadStageDto updateLeadStageDto);
    Task AssignServiceToLeadAsync(Guid id, AssignServiceDto assignServiceDto);
}
