using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages ServicePitch entities.
/// </summary>
public interface IServicePitchRepository : IRepository<ServicePitch>
{
    /// <summary>
    /// Retrieves all pitches associated with a specific service.
    /// </summary>
    /// <param name="serviceId">The ID of the service.</param>
    /// <returns>A collection of service pitches for the specified service.</returns>
    Task<IEnumerable<ServicePitch>> GetPitchesForServiceAsync(Guid serviceId);

    /// <summary>
    /// Retrieves all pitches of a specific type for a given service.
    /// </summary>
    /// <param name="serviceId">The ID of the service.</param>
    /// <param name="pitchType">The type of pitch to retrieve (e.g., 'ROI').</param>
    /// <returns>A collection of service pitches matching the criteria.</returns>
    Task<IEnumerable<ServicePitch>> GetPitchesByTypeAsync(Guid serviceId, string pitchType);
}
