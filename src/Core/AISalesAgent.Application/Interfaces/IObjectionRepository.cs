using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages Objection entities.
/// </summary>
public interface IObjectionRepository : IRepository<Objection>
{
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
}
