using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a specific piece of sales copy (a "pitch") for a given service.
/// This allows for context-specific messaging (e.g., short intros, ROI arguments).
/// </summary>
public class ServicePitch
{
    [Key]
    public Guid PitchId { get; set; }

    /// <summary>
    /// The service this pitch is associated with.
    /// </summary>
    [ForeignKey(nameof(Service))]
    public Guid ServiceId { get; set; }

    /// <summary>
    /// The type of pitch, used by the AI to select the right message. 
    /// Examples: 'ShortIntro', 'DetailedExplanation', 'ROI', 'ObjectionResponse'.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string PitchType { get; set; } = "Generic";

    /// <summary>
    /// A user-friendly title for easy identification in the UI.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string PitchTitle { get; set; } = string.Empty;

    /// <summary>
    /// The actual text content of the pitch.
    /// </summary>
    [Required]
    public string PitchText { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Service? Service { get; set; }
}
