using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages AIResponse entities.
/// This repository is primarily used for logging AI interactions.
/// </summary>
public interface IAIResponseRepository : IRepository<AIResponse>
{
    /// <summary>
    /// Retrieves all AI responses associated with a specific timeline event.
    /// (Typically a one-to-one relationship).
    /// </summary>
    /// <param name="timelineEventId">The ID of the timeline event.</param>
    /// <returns>A collection of AI responses for the event.</returns>
    Task<IEnumerable<AIResponse>> GetByTimelineEventIdAsync(Guid timelineEventId);
}
