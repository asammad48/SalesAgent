using AISalesAgent.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface ISalesTaskService
{
    Task<IEnumerable<SalesTaskDto>> GetSalesTasksAsync();
    Task<SalesTaskDto> GetSalesTaskByIdAsync(Guid id);
    Task StartTaskAsync(Guid id);
    Task PauseTaskAsync(Guid id);
    Task ResumeTaskAsync(Guid id);
    Task CancelTaskAsync(Guid id);
}
