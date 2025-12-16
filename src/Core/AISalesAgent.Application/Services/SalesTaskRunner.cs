using AISalesAgent.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services
{
    public class SalesTaskRunner : ISalesTaskRunner
    {
        private readonly ISalesTaskOrchestrator _salesTaskOrchestrator;

        public SalesTaskRunner(ISalesTaskOrchestrator salesTaskOrchestrator)
        {
            _salesTaskOrchestrator = salesTaskOrchestrator;
        }

        public async Task ExecuteAsync(Guid taskId)
        {
            await _salesTaskOrchestrator.ExecuteNextStepAsync(taskId);
        }
    }
}
