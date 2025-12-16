using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages Lead entities.
/// This interface abstracts the data access logic for leads from the application's business logic.
/// </summary>
public interface ILeadRepository
{
    /// <summary>
    /// Retrieves a lead by its unique identifier.
    /// </summary>
    /// <param name="leadId">The ID of the lead.</param>
    /// <returns>The lead entity if found; otherwise, null.</returns>
    Task<Lead?> GetByIdAsync(Guid leadId);

    /// <summary>
    /// Retrieves all leads.
    /// </summary>
    /// <returns>A collection of all lead entities.</returns>
    Task<IEnumerable<Lead>> GetAllAsync();

    /// <summary>
    /// Retrieves leads that match a specific status.
    /// </summary>
    /// <param name="status">The status to filter by (e.g., "New", "Qualified").</param>
    /// <returns>A collection of leads with the specified status.</returns>
    Task<IEnumerable<Lead>> GetLeadsByStatusAsync(string status);

    /// <summary>
    /// Retrieves leads that are in a specific sales stage.
    /// </summary>
    /// <param name="stage">The sales stage to filter by (e.g., "InitialContact").</param>
    /// <returns>A collection of leads in the specified stage.</returns>
    Task<IEnumerable<Lead>> GetLeadsBySalesStageAsync(string stage);

    /// <summary>
    /// Adds a new lead to the repository.
    /// </summary>
    /// <param name="lead">The lead entity to add.</param>
    Task AddAsync(Lead lead);

    /// <summary>
    /// Updates an existing lead in the repository.
    /// </summary>
    /// <param name="lead">The lead entity to update.</param>
    Task UpdateAsync(Lead lead);

    /// <summary>
    /// Deletes a lead from the repository by its unique identifier.
    /// </summary>
    /// <param name="leadId">The ID of the lead to delete.</param>
    Task DeleteAsync(Guid leadId);
}
