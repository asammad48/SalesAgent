using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System;
using System.Threading.Tasks;
using AISalesAgent.Core.Interfaces;
using System.Linq;

namespace AISalesAgent.Application.Services
{
    public class SalesTaskOrchestrator : ISalesTaskOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalesTaskScheduler _scheduler;
        private readonly ISalesStageEngine _salesStageEngine; // Placeholder for actual work

        public SalesTaskOrchestrator(IUnitOfWork unitOfWork, ISalesTaskScheduler scheduler, ISalesStageEngine salesStageEngine)
        {
            _unitOfWork = unitOfWork;
            _scheduler = scheduler;
            _salesStageEngine = salesStageEngine;
        }

        public async Task<SalesTask> CreateTaskAsync(string leadId, string taskType, TimeSpan? initialDelay = null)
        {
            var task = new SalesTask
            {
                LeadId = Guid.Parse(leadId),
                TaskType = taskType,
                TaskState = TaskState.PENDING,
                NextExecutionAt = DateTime.UtcNow + (initialDelay ?? TimeSpan.Zero),
                TotalSteps = 3 // Simulate a 3-step workflow
            };

            await _unitOfWork.SalesTasks.AddAsync(task);
            await _unitOfWork.CompleteAsync();

            if (initialDelay.HasValue)
            {
                _scheduler.ScheduleFollowUp(task.TaskId, initialDelay.Value);
            }
            else
            {
                _scheduler.ScheduleTask(task.TaskId);
            }

            return task;
        }

        public async Task ExecuteNextStepAsync(Guid taskId)
        {
            var task = await _unitOfWork.SalesTasks.GetByIdAsync(taskId);
            if (task == null || task.TaskState != TaskState.PENDING) return;

            task.TaskState = TaskState.RUNNING;
            task.LastExecutionAt = DateTime.UtcNow;
            task.CurrentStep++;
            task.ExecutionLogs.Add(new ExecutionLog { Message = $"Executing step {task.CurrentStep} of {task.TotalSteps}." });
            await _unitOfWork.CompleteAsync();

            try
            {
                // Placeholder for actual work for the current step.
                // await _salesStageEngine.ProcessNextStepAsync(task.Lead);

                task.ExecutionLogs.Add(new ExecutionLog { Message = $"Step {task.CurrentStep} completed successfully." });

                if (task.CurrentStep >= task.TotalSteps)
                {
                    await CompleteTaskAsync(taskId);
                }
                else
                {
                    task.TaskState = TaskState.PENDING;
                    var nextStepDelay = TimeSpan.FromMinutes(5); // Simulate delay between steps
                    task.NextExecutionAt = DateTime.UtcNow + nextStepDelay;
                    _scheduler.ScheduleFollowUp(taskId, nextStepDelay);
                    task.ExecutionLogs.Add(new ExecutionLog { Message = $"Scheduled next step in {nextStepDelay.TotalMinutes} minutes." });
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                await FailTaskAsync(taskId, ex.Message, retry: true);
            }
        }

        public async Task PauseTaskAsync(Guid taskId)
        {
            var task = await _unitOfWork.SalesTasks.GetByIdAsync(taskId);
            if (task == null) return;

            task.TaskState = TaskState.WAITING_FOR_CLIENT;
            task.NextExecutionAt = null; // Paused indefinitely
            task.ExecutionLogs.Add(new ExecutionLog { Message = "Task paused, waiting for client." });

            await _unitOfWork.CompleteAsync();
        }

        public async Task ResumeTaskAsync(Guid taskId)
        {
            var task = await _unitOfWork.SalesTasks.GetByIdAsync(taskId);
            if (task == null || task.TaskState != TaskState.WAITING_FOR_CLIENT) return;

            task.TaskState = TaskState.PENDING;
            task.NextExecutionAt = DateTime.UtcNow;
            task.ExecutionLogs.Add(new ExecutionLog { Message = "Task resumed by user." });

            _scheduler.ScheduleTask(taskId);

            await _unitOfWork.CompleteAsync();
        }

        public async Task EscalateTaskAsync(Guid taskId, string reason)
        {
            var task = await _unitOfWork.SalesTasks.GetByIdAsync(taskId);
            if (task == null) return;

            task.TaskState = TaskState.ESCALATED;
            task.NextExecutionAt = null; // Stop execution
            task.FailureReason = reason;
            task.ExecutionLogs.Add(new ExecutionLog { Message = $"Task escalated: {reason}" });

            await _unitOfWork.CompleteAsync();
        }

        public async Task CompleteTaskAsync(Guid taskId)
        {
            var task = await _unitOfWork.SalesTasks.GetByIdAsync(taskId);
            if (task == null) return;

            task.TaskState = TaskState.COMPLETED;
            task.NextExecutionAt = null;
            task.ExecutionLogs.Add(new ExecutionLog { Message = "Task completed successfully. Archiving timeline." });

            // Placeholder for archiving logic
            // await ArchiveTimelineAsync(task.LeadId);

            await _unitOfWork.CompleteAsync();
        }

        public async Task FailTaskAsync(Guid taskId, string reason, bool retry)
        {
            var task = await _unitOfWork.SalesTasks.GetByIdAsync(taskId);
            if (task == null) return;

            task.TaskState = TaskState.FAILED;
            task.FailureReason = reason;
            task.ExecutionLogs.Add(new ExecutionLog { Message = $"Task failed: {reason}" });

            if (retry)
            {
                var retryAttempt = task.ExecutionLogs.Count(log => log.Message.Contains("failed"));
                task.TaskState = TaskState.PENDING; // Reset to pending for retry
                _scheduler.ScheduleRetry(taskId, retryAttempt);
                task.NextExecutionAt = DateTime.UtcNow + TimeSpan.FromSeconds(15 * Math.Pow(4, retryAttempt - 1));
                task.ExecutionLogs.Add(new ExecutionLog { Message = $"Scheduling retry attempt {retryAttempt}." });
            }
            else
            {
                task.NextExecutionAt = null; // No more retries
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
