using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
