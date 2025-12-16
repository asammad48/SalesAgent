using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Exceptions;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services;

public class AIControlService : IAIControlService
{
    private readonly IAIResponseRepository _aiResponseRepository;
    private readonly ILeadRepository _leadRepository;
    private readonly ISalesTaskRepository _salesTaskRepository;
    private readonly ISalesTaskOrchestrator _salesTaskOrchestrator;
    private readonly IUnitOfWork _unitOfWork;

    public AIControlService(
        IAIResponseRepository aiResponseRepository,
        ILeadRepository leadRepository,
        ISalesTaskRepository salesTaskRepository,
        ISalesTaskOrchestrator salesTaskOrchestrator,
        IUnitOfWork unitOfWork)
    {
        _aiResponseRepository = aiResponseRepository;
        _leadRepository = leadRepository;
        _salesTaskRepository = salesTaskRepository;
        _salesTaskOrchestrator = salesTaskOrchestrator;
        _unitOfWork = unitOfWork;
    }

    public async Task ApproveResponseAsync(ApproveResponseDto approveResponseDto)
    {
        var response = await _aiResponseRepository.GetByIdAsync(approveResponseDto.ResponseId);
        if (response == null)
        {
            throw new NotFoundException(nameof(AIResponse), approveResponseDto.ResponseId);
        }

        var task = await _salesTaskRepository.FindAsync(st => st.ExecutionLogs.Any(el => el.TimelineEventId == response.TimelineEventId));
        if (task != null && task.TaskState == TaskState.WAITING_FOR_CLIENT)
        {
            await _salesTaskOrchestrator.ResumeTaskAsync(task.TaskId);
        }
    }

    public async Task RejectResponseAsync(RejectResponseDto rejectResponseDto)
    {
        var response = await _aiResponseRepository.GetByIdAsync(rejectResponseDto.ResponseId);
        if (response == null)
        {
            throw new NotFoundException(nameof(AIResponse), rejectResponseDto.ResponseId);
        }

        var task = await _salesTaskRepository.FindAsync(st => st.ExecutionLogs.Any(el => el.TimelineEventId == response.TimelineEventId));
        if (task != null)
        {
            await _salesTaskOrchestrator.EscalateTaskAsync(task.TaskId, rejectResponseDto.Reason);
        }
    }

    public async Task EscalateAsync(EscalateDto escalateDto)
    {
        var lead = await _leadRepository.GetByIdAsync(escalateDto.LeadId);
        if (lead == null)
        {
            throw new NotFoundException(nameof(Lead), escalateDto.LeadId);
        }

        var task = (await _salesTaskRepository.GetWhereAsync(t => t.LeadId == escalateDto.LeadId && t.TaskState != TaskState.COMPLETED && t.TaskState != TaskState.CANCELLED))
                   .OrderByDescending(t => t.CreatedAt)
                   .FirstOrDefault();

        if (task != null)
        {
            await _salesTaskOrchestrator.EscalateTaskAsync(task.TaskId, escalateDto.Reason);
        }
    }

    public async Task TakeoverAsync(TakeoverDto takeoverDto)
    {
        var lead = await _leadRepository.GetByIdAsync(takeoverDto.LeadId);
        if (lead == null)
        {
            throw new NotFoundException(nameof(Lead), takeoverDto.LeadId);
        }

        lead.AssignedTo = takeoverDto.HumanAgentId;
        lead.UpdatedAt = DateTime.UtcNow;

        var activeTasks = await _salesTaskRepository.GetWhereAsync(t => t.LeadId == takeoverDto.LeadId && (t.TaskState == TaskState.RUNNING || t.TaskState == TaskState.PENDING));
        foreach (var task in activeTasks)
        {
            await _salesTaskOrchestrator.PauseTaskAsync(task.TaskId);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
