using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages SalesTask entities.
/// </summary>
public interface ISalesTaskRepository : IRepository<SalesTask>
{
    /// <summary>
    /// Retrieves all tasks for a specific lead.
    /// </summary>
    /// <param name="leadId">The ID of the lead.</param>
    /// <returns>A collection of sales tasks for the specified lead.</returns>
    Task<IEnumerable<SalesTask>> GetTasksForLeadAsync(Guid leadId);

    /// <summary>
    /// Retrieves all tasks that are in a specific state.
    /// </summary>
    /// <param name="state">The state to filter by (e.g., 'PENDING', 'ESCALATED').</param>
    /// <returns>A collection of sales tasks in the specified state.</returns>
    Task<IEnumerable<SalesTask>> GetTasksByStateAsync(TaskState state);

    /// <summary>
    /// Retrieves all pending tasks whose scheduled execution time has passed.
    /// </summary>
    /// <returns>A collection of due tasks ready for processing.</returns>
    Task<IEnumerable<SalesTask>> GetDuePendingTasksAsync();
}
