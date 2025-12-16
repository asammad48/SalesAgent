using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services
{
    public class SalesTaskOrchestrator : ISalesTaskOrchestrator
    {
        private readonly ISalesTaskRepository _salesTaskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalesTaskOrchestrator(ISalesTaskRepository salesTaskRepository, IUnitOfWork unitOfWork)
        {
            _salesTaskRepository = salesTaskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SalesTask> CreateTaskAsync(SalesTaskCreationRequest request)
        {
            var task = new SalesTask
            {
                LeadId = request.LeadId,
                TaskType = request.TaskType,
                TaskState = TaskState.PENDING,
                ScheduledAt = request.ScheduledAt
            };

            await _salesTaskRepository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return task;
        }

        public async Task ResumeTaskAsync(Guid taskId)
        {
            var task = await _salesTaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.TaskState = TaskState.RUNNING;
                _salesTaskRepository.Update(task);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task PauseTaskAsync(Guid taskId)
        {
            var task = await _salesTaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.TaskState = TaskState.WAITING_FOR_CLIENT;
                _salesTaskRepository.Update(task);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task EscalateTaskAsync(Guid taskId)
        {
            var task = await _salesTaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.TaskState = TaskState.ESCALATED;
                _salesTaskRepository.Update(task);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CompleteTaskAsync(Guid taskId)
        {
            var task = await _salesTaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.TaskState = TaskState.COMPLETED;
                task.CompletedAt = DateTime.UtcNow;
                _salesTaskRepository.Update(task);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task FailTaskAsync(Guid taskId, string reason)
        {
            var task = await _salesTaskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.TaskState = TaskState.FAILED;
                task.FailureReason = reason;
                _salesTaskRepository.Update(task);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
