using AISalesAgent.Domain.Entities;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages AgentSettings.
/// Typically, there will be only one settings entity in the database.
/// </summary>
public interface IAgentSettingsRepository
{
    /// <summary>
    /// Retrieves the current (and likely only) agent settings.
    /// </summary>
    /// <returns>The active agent settings entity.</returns>
    Task<AgentSettings?> GetSettingsAsync();

    /// <summary>
    /// Creates or updates the agent settings.
    /// This is the primary method for managing the single settings entity.
    /// </summary>
    /// <param name="settings">The settings entity to save.</param>
    Task SaveSettingsAsync(AgentSettings settings);
}
