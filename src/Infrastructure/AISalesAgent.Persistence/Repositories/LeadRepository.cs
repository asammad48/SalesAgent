using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using AISalesAgent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Infrastructure.Persistence.Repositories
{
    public class LeadRepository : Repository<Lead>, ILeadRepository
    {
        public LeadRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lead>> GetLeadsByStatusAsync(string status)
        {
            return await _context.Leads
                .Where(l => l.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetLeadsBySalesStageAsync(SalesStage stage)
        {
            return await _context.Leads
                .Where(l => l.SalesStage == stage)
                .ToListAsync();
        }
    }
}
