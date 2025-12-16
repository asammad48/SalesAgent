using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a Call to Action (CTA) that the AI can use in its communication.
/// </summary>
public class CTA
{
    [Key]
    public Guid CTAId { get; set; }

    /// <summary>
    /// The text of the Call to Action (e.g., "Would you like to schedule a call?").
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string CTAText { get; set; } = string.Empty;

    /// <summary>
    /// The type of CTA, for logical grouping (e.g., 'ScheduleMeeting', 'RequestInfo').
    /// </summary>
    [MaxLength(50)]
    public string? CTAType { get; set; }

    /// <summary>
    /// Whether this CTA is currently available for the AI to use.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
