using AISalesAgent.Application.DTOs;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface IEscalationService
    {
        Task<bool> CheckForEscalationAsync(EscalationCheckRequest request);
    }
}
