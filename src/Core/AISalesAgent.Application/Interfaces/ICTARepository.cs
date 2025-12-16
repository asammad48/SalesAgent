using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages CTA (Call to Action) entities.
/// </summary>
public interface ICTARepository
{
    /// <summary>
    /// Retrieves a CTA by its unique identifier.
    /// </summary>
    /// <param name="ctaId">The ID of the CTA.</param>
    /// <returns>The CTA entity if found; otherwise, null.</returns>
    Task<CTA?> GetByIdAsync(Guid ctaId);

    /// <summary>
    /// Retrieves all CTAs.
    /// </summary>
    /// <returns>A collection of all CTA entities.</returns>
    Task<IEnumerable<CTA>> GetAllAsync();

    /// <summary>
    /// Retrieves all active CTAs.
    /// </summary>
    /// <returns>A collection of all active CTAs.</returns>
    Task<IEnumerable<CTA>> GetActiveCTAsAsync();

    /// <summary>
    /// Retrieves all active CTAs of a specific type.
    /// </summary>
    /// <param name="ctaType">The type of CTA to retrieve.</param>
    /// <returns>A collection of active CTAs of the specified type.</returns>
    Task<IEnumerable<CTA>> GetActiveCTAsByTypeAsync(string ctaType);

    /// <summary>
    /// Adds a new CTA to the repository.
    /// </summary>
    /// <param name="cta">The CTA entity to add.</param>
    Task AddAsync(CTA cta);

    /// <summary>
    /// Updates an existing CTA in the repository.
    /// </summary>
    /// <param name="cta">The CTA entity to update.</param>
    Task UpdateAsync(CTA cta);

    /// <summary>
    /// Deletes a CTA from the repository.
    /// </summary>
    /// <param name="ctaId">The ID of the CTA to delete.</param>
    Task DeleteAsync(Guid ctaId);
}
