using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages SalesTimeline entities.
/// </s ummary>
public interface ISalesTimelineRepository : IRepository<SalesTimeline>
{
    /// <summary>
    /// Retrieves the entire chronological timeline of events for a specific lead.
    /// </summary>
    /// <param name="leadId">The ID of the lead.</param>
    /// <returns>A collection of timeline events, ordered by timestamp.</returns>
    Task<IEnumerable<SalesTimeline>> GetTimelineForLeadAsync(Guid leadId);
}
