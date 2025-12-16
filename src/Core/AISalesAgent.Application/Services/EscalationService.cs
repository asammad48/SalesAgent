using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services
{
    public class EscalationService : IEscalationService
    {
        private readonly IEscalationRuleRepository _escalationRuleRepository;
        private readonly ISalesTaskOrchestrator _salesTaskOrchestrator;
        private readonly ISalesStageEngine _salesStageEngine;

        public EscalationService(
            IEscalationRuleRepository escalationRuleRepository,
            ISalesTaskOrchestrator salesTaskOrchestrator,
            ISalesStageEngine salesStageEngine)
        {
            _escalationRuleRepository = escalationRuleRepository;
            _salesTaskOrchestrator = salesTaskOrchestrator;
            _salesStageEngine = salesStageEngine;
        }

        public async Task<bool> CheckForEscalationAsync(EscalationCheckRequest request)
        {
            if (request.Lead == null || request.SalesTask == null)
            {
                return false;
            }

            var escalationRules = await _escalationRuleRepository.GetActiveRulesAsync();

            foreach (var rule in escalationRules)
            {
                if (MatchesRule(rule, request))
                {
                    await EscalateAsync(request.Lead, request.SalesTask);
                    return true;
                }
            }

            return false;
        }

        private bool MatchesRule(EscalationRule rule, EscalationCheckRequest request)
        {
            return rule.ConditionType switch
            {
                "Keyword" => request.Message?.Contains(rule.ConditionValue) ?? false,
                "DealValue" => decimal.TryParse(rule.ConditionValue, out var dealValue) && (request.Lead?.LeadScore ?? 0) > dealValue,
                _ => false,
            };
        }

        private async Task EscalateAsync(Lead lead, SalesTask salesTask)
        {
            await _salesTaskOrchestrator.EscalateTaskAsync(salesTask.TaskId);

            var stageTransitionRequest = new StageTransitionRequest
            {
                Lead = lead,
                NewStage = Domain.Enums.SalesStage.ESCALATED
            };
            await _salesStageEngine.TransitionToStageAsync(stageTransitionRequest);
        }
    }
}
