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
    public class ObjectionRepository : Repository<Objection>, IObjectionRepository
    {
        public ObjectionRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Objection>> GetActiveObjectionsAsync()
        {
            return await _context.Objections.Where(o => o.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Objection>> FindMatchingObjectionsAsync(string query)
        {
            return await _context.Objections
                .Where(o => o.IsActive && (o.ObjectionName.Contains(query) || (o.Keywords != null && o.Keywords.Contains(query))))
                .ToListAsync();
        }
    }
}
