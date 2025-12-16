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
    public class ServicePitchRepository : Repository<ServicePitch>, IServicePitchRepository
    {
        public ServicePitchRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ServicePitch>> GetPitchesForServiceAsync(Guid serviceId)
        {
            return await _context.ServicePitches
                .Where(p => p.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServicePitch>> GetPitchesByTypeAsync(Guid serviceId, string pitchType)
        {
            return await _context.ServicePitches
                .Where(p => p.ServiceId == serviceId && p.PitchType == pitchType)
                .ToListAsync();
        }
    }
}
