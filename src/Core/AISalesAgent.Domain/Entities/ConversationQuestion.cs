using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a question the AI can ask a lead to gather information or guide the conversation.
/// </summary>
public class ConversationQuestion
{
    [Key]
    public Guid QuestionId { get; set; }

    /// <summary>
    /// The full text of the question.
    /// </summary>
    [Required]
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// The type of question, for logical grouping (e.g., 'OpenEnded', 'Budget', 'Timeline').
    /// </summary>
    [MaxLength(50)]
    public string? QuestionType { get; set; }

    /// <summary>
    /// Whether this question is currently available for the AI to use.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
