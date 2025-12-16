using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Exceptions;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services;

public class SalesTaskService : ISalesTaskService
{
    private readonly ISalesTaskRepository _salesTaskRepository;
    private readonly ISalesTaskOrchestrator _salesTaskOrchestrator;
    private readonly IUnitOfWork _unitOfWork;

    public SalesTaskService(
        ISalesTaskRepository salesTaskRepository,
        ISalesTaskOrchestrator salesTaskOrchestrator,
        IUnitOfWork unitOfWork)
    {
        _salesTaskRepository = salesTaskRepository;
        _salesTaskOrchestrator = salesTaskOrchestrator;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SalesTaskDto>> GetSalesTasksAsync()
    {
        var tasks = await _salesTaskRepository.GetAllAsync();
        return tasks.Select(t => new SalesTaskDto
        {
            TaskId = t.TaskId,
            LeadId = t.LeadId,
            TaskType = t.TaskType,
            TaskState = t.TaskState.ToString(),
            CurrentStep = t.CurrentStep,
            TotalSteps = t.TotalSteps,
            LastExecutionAt = t.LastExecutionAt,
            NextExecutionAt = t.NextExecutionAt,
            FailureReason = t.FailureReason,
            CreatedAt = t.CreatedAt
        });
    }

    public async Task<SalesTaskDto?> GetSalesTaskByIdAsync(Guid id)
    {
        var t = await _salesTaskRepository.GetByIdAsync(id);
        if (t == null) return null;

        return new SalesTaskDto
        {
            TaskId = t.TaskId,
            LeadId = t.LeadId,
            TaskType = t.TaskType,
            TaskState = t.TaskState.ToString(),
            CurrentStep = t.CurrentStep,
            TotalSteps = t.TotalSteps,
            LastExecutionAt = t.LastExecutionAt,
            NextExecutionAt = t.NextExecutionAt,
            FailureReason = t.FailureReason,
            CreatedAt = t.CreatedAt
        };
    }

    private async Task<SalesTask> GetTaskOrThrowAsync(Guid id)
    {
        var task = await _salesTaskRepository.GetByIdAsync(id);
        if (task == null)
        {
            throw new NotFoundException(nameof(SalesTask), id);
        }
        return task;
    }

    public async Task StartTaskAsync(Guid id)
    {
        await GetTaskOrThrowAsync(id);
        await _salesTaskOrchestrator.ExecuteNextStepAsync(id);
    }

    public async Task PauseTaskAsync(Guid id)
    {
        await GetTaskOrThrowAsync(id);
        await _salesTaskOrchestrator.PauseTaskAsync(id);
    }

    public async Task ResumeTaskAsync(Guid id)
    {
        await GetTaskOrThrowAsync(id);
        await _salesTaskOrchestrator.ResumeTaskAsync(id);
    }

    public async Task CancelTaskAsync(Guid id)
    {
        var task = await GetTaskOrThrowAsync(id);
        task.TaskState = TaskState.CANCELLED;
        await _unitOfWork.SaveChangesAsync();
    }
}
