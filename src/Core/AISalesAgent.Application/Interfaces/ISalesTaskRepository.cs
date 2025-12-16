using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages SalesTask entities.
/// </summary>
public interface ISalesTaskRepository
{
    /// <summary>
    /// Retrieves a sales task by its unique identifier.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>The sales task entity if found; otherwise, null.</returns>
    Task<SalesTask?> GetByIdAsync(Guid taskId);

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
    Task<IEnumerable<SalesTask>> GetTasksByStateAsync(string state);

    /// <summary>
    /// Retrieves all pending tasks whose scheduled execution time has passed.
    /// </summary>
    /// <returns>A collection of due tasks ready for processing.</returns>
    Task<IEnumerable<SalesTask>> GetDuePendingTasksAsync();

    /// <summary>
    /// Adds a new sales task to the repository.
    /// </summary>
    /// <param name="task">The sales task entity to add.</param>
    Task AddAsync(SalesTask task);

    /// <summary>
    /// Updates an existing sales task in the repository.
    /// </summary>
    /// <param name="task">The sales task entity to update.</param>
    Task UpdateAsync(SalesTask task);
}
