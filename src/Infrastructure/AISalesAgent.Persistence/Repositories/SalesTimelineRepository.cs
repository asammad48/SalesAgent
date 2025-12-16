using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Infrastructure.Persistence.Repositories
{
    public class SalesTimelineRepository : Repository<SalesTimeline>, ISalesTimelineRepository
    {
        public SalesTimelineRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SalesTimeline>> GetTimelineForLeadAsync(Guid leadId)
        {
            return await _context.SalesTimelines
                .Where(e => e.LeadId == leadId)
                .OrderBy(e => e.EventTimestamp)
                .ToListAsync();
        }
    }
}
