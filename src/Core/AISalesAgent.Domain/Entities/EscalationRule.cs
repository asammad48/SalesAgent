using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Defines a rule that determines when the AI must escalate a conversation to a human.
/// </summary>
public class EscalationRule
{
    [Key]
    public Guid RuleId { get; set; }

    /// <summary>
    /// A user-friendly name for the rule (e.g., "High-Value Deal Escalation").
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// The type of condition to check for (e.g., 'Keyword', 'Sentiment', 'DealValue').
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ConditionType { get; set; } = string.Empty;

    /// <summary>
    /// The value to check against the condition (e.g., a specific keyword, a sentiment score threshold).
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string ConditionValue { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether this rule is currently active and being checked.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
