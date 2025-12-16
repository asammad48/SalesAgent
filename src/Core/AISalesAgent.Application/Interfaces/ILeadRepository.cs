using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages Lead entities.
/// This interface abstracts the data access logic for leads from the application's business logic.
/// </summary>
public interface ILeadRepository : IRepository<Lead>
{
    /// <summary>
    /// Retrieves leads that match a specific status.
    /// </summary>
    /// <param name="status">The status to filter by (e.g., "New", "Qualified").</param>
    /// <returns>A collection of leads with the specified status.</returns>
    Task<IEnumerable<Lead>> GetLeadsByStatusAsync(string status);

    /// <summary>
    /// Retrieves leads that are in a specific sales stage.
    /// </summary>
    /// <param name="stage">The sales stage to filter by (e.g., "InitialContact").</param>
    /// <returns>A collection of leads in the specified stage.</returns>
    Task<IEnumerable<Lead>> GetLeadsBySalesStageAsync(SalesStage stage);

    /// <summary>
    /// Checks if a lead exists with the given email or phone number.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="phone">The phone number to check.</param>
    /// <returns>True if a lead exists, otherwise false.</returns>
    Task<bool> ExistsByEmailOrPhoneAsync(string? email, string? phone);
}
