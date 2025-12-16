using AISalesAgent.Application.DTOs;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface ISalesStageEngine
    {
        Task<bool> TransitionToStageAsync(StageTransitionRequest request);
    }
}
