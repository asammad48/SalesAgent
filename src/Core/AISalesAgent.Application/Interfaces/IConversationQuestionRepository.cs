using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

/// <summary>
/// Defines the contract for the repository that manages ConversationQuestion entities.
/// </summary>
public interface IConversationQuestionRepository
{
    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <param name="questionId">The ID of the question.</param>
    /// <returns>The question entity if found; otherwise, null.</returns>
    Task<ConversationQuestion?> GetByIdAsync(Guid questionId);

    /// <summary>
    /// Retrieves all conversation questions.
    /// </summary>
    /// <returns>A collection of all question entities.</returns>
    Task<IEnumerable<ConversationQuestion>> GetAllAsync();

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

    /// <summary>
    /// Adds a new question to the repository.
    /// </summary>
    /// <param name="question">The question entity to add.</param>
    Task AddAsync(ConversationQuestion question);

    /// <summary>
    /// Updates an existing question in the repository.
    /// </summary>
    /// <param name="question">The question entity to update.</param>
    Task UpdateAsync(ConversationQuestion question);

    /// <summary>
    /// Deletes a question from the repository.
    /// </summary>
    /// <param name="questionId">The ID of the question to delete.</param>
    Task DeleteAsync(Guid questionId);
}
