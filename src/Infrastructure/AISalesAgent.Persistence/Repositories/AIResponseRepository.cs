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
    public class AIResponseRepository : Repository<AIResponse>, IAIResponseRepository
    {
        public AIResponseRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AIResponse>> GetByTimelineEventIdAsync(Guid timelineEventId)
        {
            return await _context.AIResponses
                .Where(r => r.TimelineEventId == timelineEventId)
                .ToListAsync();
        }
    }
}
