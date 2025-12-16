using AISalesAgent.Application.DTOs;
using AISalesAgent.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface ISalesTaskOrchestrator
    {
        Task<SalesTask> CreateTaskAsync(SalesTaskCreationRequest request);
        Task ResumeTaskAsync(Guid taskId);
        Task PauseTaskAsync(Guid taskId);
        Task EscalateTaskAsync(Guid taskId);
        Task CompleteTaskAsync(Guid taskId);
        Task FailTaskAsync(Guid taskId, string reason);
    }
}
