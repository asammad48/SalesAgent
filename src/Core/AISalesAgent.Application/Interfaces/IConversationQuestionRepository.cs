using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages ConversationQuestion entities.
/// </summary>
public interface IConversationQuestionRepository : IRepository<ConversationQuestion>
{
    /// <summary>
    /// Retrieves all active conversation questions.
    /// </summary>
    /// <returns>A collection of all active question entities.</returns>
    Task<IEnumerable<ConversationQuestion>> GetActiveQuestionsAsync();

    /// <summary>
    /// Retrieves all active questions of a specific type.
    /// </summary>
    /// <param name="questionType">The type of question to retrieve (e.g., 'Budget').</param>
    /// <returns>A collection of active questions of the specified type.</returns>
    Task<IEnumerable<ConversationQuestion>> GetActiveQuestionsByTypeAsync(string questionType);
}
