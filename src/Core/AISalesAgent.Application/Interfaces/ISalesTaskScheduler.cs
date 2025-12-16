using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface ISalesTaskScheduler
    {
        void ScheduleTask(Guid taskId);
        void ScheduleFollowUp(Guid taskId, TimeSpan delay);
        void ScheduleRetry(Guid taskId, int retryAttempt);
    }
}
