using AISalesAgent.Application.Interfaces;
using Hangfire;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services
{
    public class SalesTaskScheduler : ISalesTaskScheduler
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public SalesTaskScheduler(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public void ScheduleTask(Guid taskId)
        {
            _backgroundJobClient.Enqueue<ISalesTaskRunner>(runner => runner.ExecuteAsync(taskId));
        }

        public void ScheduleFollowUp(Guid taskId, TimeSpan delay)
        {
            _backgroundJobClient.Schedule<ISalesTaskRunner>(runner => runner.ExecuteAsync(taskId), delay);
        }

        public void ScheduleRetry(Guid taskId, int retryAttempt)
        {
            // Exponential backoff: 15s, 1m, 5m, 15m, 30m, 1h, etc.
            var delay = TimeSpan.FromSeconds(15 * Math.Pow(4, retryAttempt - 1));
            _backgroundJobClient.Schedule<ISalesTaskRunner>(runner => runner.ExecuteAsync(taskId), delay);
        }
    }
}
