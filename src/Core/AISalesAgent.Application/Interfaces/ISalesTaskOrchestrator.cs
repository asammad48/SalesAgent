using AISalesAgent.Application.DTOs;
using AISalesAgent.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface ISalesTaskOrchestrator
    {
        Task<SalesTask> CreateTaskAsync(string leadId, string taskType, TimeSpan? initialDelay = null);
        Task ExecuteNextStepAsync(Guid taskId);
        Task PauseTaskAsync(Guid taskId);
        Task ResumeTaskAsync(Guid taskId);
        Task EscalateTaskAsync(Guid taskId, string reason);
        Task CompleteTaskAsync(Guid taskId);
        Task FailTaskAsync(Guid taskId, string reason, bool retry);
    }
}
