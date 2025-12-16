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
    public class EscalationRuleRepository : Repository<EscalationRule>, IEscalationRuleRepository
    {
        public EscalationRuleRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EscalationRule>> GetActiveRulesAsync()
        {
            return await _context.EscalationRules.Where(r => r.IsActive).ToListAsync();
        }
    }
}
