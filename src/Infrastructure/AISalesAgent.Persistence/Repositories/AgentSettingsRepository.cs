using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AISalesAgent.Infrastructure.Persistence.Repositories
{
    public class AgentSettingsRepository : IAgentSettingsRepository
    {
        private readonly SalesAgentDbContext _context;

        public AgentSettingsRepository(SalesAgentDbContext context)
        {
            _context = context;
        }

        public async Task<AgentSettings?> GetSettingsAsync()
        {
            return await _context.AgentSettings.FirstOrDefaultAsync();
        }

        public async Task SaveSettingsAsync(AgentSettings settings)
        {
            var existingSettings = await GetSettingsAsync();
            if (existingSettings == null)
            {
                await _context.AgentSettings.AddAsync(settings);
            }
            else
            {
                settings.UpdatedAt = System.DateTime.UtcNow;
                _context.Entry(existingSettings).CurrentValues.SetValues(settings);
            }
            await _context.SaveChangesAsync();
        }
    }
}
