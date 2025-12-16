using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages EscalationRule entities.
/// </summary>
public interface IEscalationRuleRepository
{
    /// <summary>
    /// Retrieves an escalation rule by its unique identifier.
    /// </summary>
    /// <param name="ruleId">The ID of the rule.</param>
    /// <returns>The escalation rule entity if found; otherwise, null.</returns>
    Task<EscalationRule?> GetByIdAsync(Guid ruleId);

    /// <summary>
    /// Retrieves all escalation rules.
    /// </summary>
    /// <returns>A collection of all escalation rule entities.</returns>
    Task<IEnumerable<EscalationRule>> GetAllAsync();
    
    /// <summary>
    /// Retrieves all active escalation rules.
    /// </summary>
    /// <returns>A collection of all active escalation rule entities.</returns>
    Task<IEnumerable<EscalationRule>> GetActiveRulesAsync();

    /// <summary>
    /// Adds a new escalation rule to the repository.
    /// </summary>
    /// <param name="rule">The escalation rule entity to add.</param>
    Task AddAsync(EscalationRule rule);

    /// <summary>
    /// Updates an existing escalation rule in the repository.
    /// </summary>
    /// <param name="rule">The escalation rule entity to update.</param>
    Task UpdateAsync(EscalationRule rule);

    /// <summary>
    /// Deletes an escalation rule from the repository.
    /// </summary>
    /// <param name="ruleId">The ID of the rule to delete.</param>
    Task DeleteAsync(Guid ruleId);
}
