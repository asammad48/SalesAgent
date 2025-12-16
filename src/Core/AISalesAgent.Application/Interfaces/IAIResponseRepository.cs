using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages AIResponse entities.
/// This repository is primarily used for logging AI interactions.
/// </summary>
public interface IAIResponseRepository
{
    /// <summary>
    /// Retrieves an AI response log by its unique identifier.
    /// </summary>
    /// <param name="responseId">The ID of the AI response.</param>
    /// <returns>The AI response entity if found; otherwise, null.</returns>
    Task<AIResponse?> GetByIdAsync(Guid responseId);

    /// <summary>
    /// Retrieves all AI responses associated with a specific timeline event.
    /// (Typically a one-to-one relationship).
    /// </summary>
    /// <param name="timelineEventId">The ID of the timeline event.</param>
    /// <returns>A collection of AI responses for the event.</returns>
    Task<IEnumerable<AIResponse>> GetByTimelineEventIdAsync(Guid timelineEventId);

    /// <summary>
    /// Adds a new AI response log to the repository.
    /// </summary>
    /// <param name="response">The AI response entity to add.</param>
    Task AddAsync(AIResponse response);
}
