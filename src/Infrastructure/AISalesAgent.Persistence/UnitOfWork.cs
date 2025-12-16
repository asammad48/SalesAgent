using AISalesAgent.Application.Interfaces;
using System.Threading.Tasks;

namespace AISalesAgent.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesAgentDbContext _context;

        public UnitOfWork(SalesAgentDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
