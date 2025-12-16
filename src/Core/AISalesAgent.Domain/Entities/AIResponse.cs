using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Logs the raw prompts sent to the AI model and the responses received.
/// This is crucial for auditing, debugging, and fine-tuning AI performance.
/// </summary>
public class AIResponse
{
    [Key]
    public Guid ResponseId { get; set; }

    /// <summary>
    /// The specific timeline event that this AI interaction is part of.
    /// </summary>
    [ForeignKey(nameof(SalesTimeline))]
    public Guid TimelineEventId { get; set; }

    /// <summary>
    /// The full, compiled prompt that was sent to the language model.
    /// </summary>
    [Required]
    public string CompiledPrompt { get; set; } = string.Empty;

    /// <summary>
    /// The raw response text (e.g., JSON) received from the language model.
    /// </summary>
    [Required]
    public string ResponseText { get; set; } = string.Empty;

    /// <summary>
    /// The name of the AI model used for this interaction (e.g., 'gpt-4-turbo').
    /// </summary>
    [MaxLength(100)]
    public string? ModelUsed { get; set; }

    /// <summary>
    /// The time taken to process the request, in milliseconds.
    /// </summary>
    public int ProcessingTimeMs { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public virtual SalesTimeline? SalesTimeline { get; set; }
}
