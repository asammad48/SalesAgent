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
    public class SalesTaskRepository : Repository<SalesTask>, ISalesTaskRepository
    {
        public SalesTaskRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SalesTask>> GetTasksForLeadAsync(Guid leadId)
        {
            return await _context.SalesTasks
                .Where(t => t.LeadId == leadId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SalesTask>> GetTasksByStateAsync(string state)
        {
            return await _context.SalesTasks
                .Where(t => t.TaskState == state)
                .ToListAsync();
        }

        public async Task<IEnumerable<SalesTask>> GetDuePendingTasksAsync()
        {
            return await _context.SalesTasks
                .Where(t => t.TaskState == "PENDING" && t.ScheduledAt <= DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
