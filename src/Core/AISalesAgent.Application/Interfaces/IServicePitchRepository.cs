using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages ServicePitch entities.
/// </summary>
public interface IServicePitchRepository
{
    /// <summary>
    /// Retrieves a service pitch by its unique identifier.
    /// </summary>
    /// <param name="pitchId">The ID of the pitch.</param>
    /// <returns>The service pitch entity if found; otherwise, null.</returns>
    Task<ServicePitch?> GetByIdAsync(Guid pitchId);

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

    /// <summary>
    /// Adds a new service pitch to the repository.
    /// </summary>
    /// <param name="pitch">The service pitch entity to add.</param>
    Task AddAsync(ServicePitch pitch);

    /// <summary>
    /// Updates an existing service pitch in the repository.
    /// </summary>
    /// <param name="pitch">The service pitch entity to update.</param>
    Task UpdateAsync(ServicePitch pitch);

    /// <summary>
    /// Deletes a service pitch from the repository.
    /// </summary>
    /// <param name="pitchId">The ID of the pitch to delete.</param>
    Task DeleteAsync(Guid pitchId);
}
