using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages SalesTimeline entities.
/// </summary>
public interface ISalesTimelineRepository
{
    /// <summary>
    /// Retrieves a timeline event by its unique identifier.
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>The timeline event entity if found; otherwise, null.</returns>
    Task<SalesTimeline?> GetByIdAsync(Guid eventId);

    /// <summary>
    /// Retrieves the entire chronological timeline of events for a specific lead.
    /// </summary>
    /// <param name="leadId">The ID of the lead.</param>
    /// <returns>A collection of timeline events, ordered by timestamp.</returns>
    Task<IEnumerable<SalesTimeline>> GetTimelineForLeadAsync(Guid leadId);

    /// <summary>
    /// Adds a new event to a lead's timeline.
    /// This is the primary method for recording interactions.
    /// </summary>
    /// <param name="timelineEvent">The timeline event entity to add.</param>
    Task AddAsync(SalesTimeline timelineEvent);
}
