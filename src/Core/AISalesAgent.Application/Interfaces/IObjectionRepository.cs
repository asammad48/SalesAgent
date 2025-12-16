using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages Objection entities.
/// </summary>
public interface IObjectionRepository
{
    /// <summary>
    /// Retrieves an objection by its unique identifier.
    /// </summary>
    /// <param name="objectionId">The ID of the objection.</param>
    /// <returns>The objection entity if found; otherwise, null.</returns>
    Task<Objection?> GetByIdAsync(Guid objectionId);

    /// <summary>
    /// Retrieves all objections.
    /// </summary>
    /// <returns>A collection of all objection entities.</returns>
    Task<IEnumerable<Objection>> GetAllAsync();

    /// <summary>
    /// Retrieves all active objections that the AI is permitted to use.
    /// </summary>
    /// <returns>A collection of active objection entities.</returns>
    Task<IEnumerable<Objection>> GetActiveObjectionsAsync();

    /// <summary>
    /// Searches for objections that match a given query or set of keywords.
    /// This is a key method for the AI to find relevant objection responses.
    /// </summary>
    /// <param name="query">The text or keywords from a lead's message.</param>
    /// <returns>A collection of matching objections.</returns>
    Task<IEnumerable<Objection>> FindMatchingObjectionsAsync(string query);

    /// <summary>
    /// Adds a new objection to the repository.
    /// </summary>
    /// <param name="objection">The objection entity to add.</param>
    Task AddAsync(Objection objection);

    /// <summary>
    /// Updates an existing objection in the repository.
    /// </summary>
    /// <param name="objection">The objection entity to update.</param>
    Task UpdateAsync(Objection objection);

    /// <summary>
    /// Deletes an objection from the repository.
    /// </summary>
    /// <param name="objectionId">The ID of the objection to delete.</param>
    Task DeleteAsync(Guid objectionId);
}
