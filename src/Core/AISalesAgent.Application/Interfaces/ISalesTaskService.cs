using AISalesAgent.Application.DTOs;

namespace AISalesAgent.Application.Interfaces;

public interface ISalesTaskService
{
    Task<IEnumerable<SalesTaskDto>> GetSalesTasksAsync();
    Task<SalesTaskDto?> GetSalesTaskByIdAsync(Guid id);
    Task StartTaskAsync(Guid id);
    Task PauseTaskAsync(Guid id);
    Task ResumeTaskAsync(Guid id);
    Task CancelTaskAsync(Guid id);
}
