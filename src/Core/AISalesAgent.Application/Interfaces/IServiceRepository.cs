using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages Service entities.
/// </summary>
public interface IServiceRepository
{
    /// <summary>
    /// Retrieves a service by its unique identifier.
    /// </summary>
    /// <param name="serviceId">The ID of the service.</param>
    /// <returns>The service entity if found; otherwise, null.</returns>
    Task<Service?> GetByIdAsync(Guid serviceId);

    /// <summary>
    /// Retrieves all services.
    /// </summary>
    /// <returns>A collection of all service entities.</returns>
    Task<IEnumerable<Service>> GetAllAsync();
    
    /// <summary>
    /// Retrieves all active services.
    /// </summary>
    /// <returns>A collection of all active service entities.</returns>
    Task<IEnumerable<Service>> GetActiveServicesAsync();

    /// <summary>
    /// Adds a new service to the repository.
    /// </summary>
    /// <param name="service">The service entity to add.</param>
    Task AddAsync(Service service);

    /// <summary>
    /// Updates an existing service in the repository.
    /// </summary>
    /// <param name="service">The service entity to update.</param>
    Task UpdateAsync(Service service);

    /// <summary>
    /// Deletes a service from the repository by its unique identifier.
    /// </summary>
    /// <param name="serviceId">The ID of the service to delete.</param>
    Task DeleteAsync(Guid serviceId);
}
