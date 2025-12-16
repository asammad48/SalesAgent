using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages EscalationRule entities.
/// </summary>
public interface IEscalationRuleRepository : IRepository<EscalationRule>
{
    /// <summary>
    /// Retrieves all active escalation rules.
    /// </summary>
    /// <returns>A collection of all active escalation rule entities.</returns>
    Task<IEnumerable<EscalationRule>> GetActiveRulesAsync();
}
