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
    public class CTARepository : Repository<CTA>, ICTARepository
    {
        public CTARepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CTA>> GetActiveCTAsAsync()
        {
            return await _context.CTAs.Where(c => c.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<CTA>> GetActiveCTAsByTypeAsync(string ctaType)
        {
            return await _context.CTAs
                .Where(c => c.IsActive && c.CTAType == ctaType)
                .ToListAsync();
        }
    }
}
