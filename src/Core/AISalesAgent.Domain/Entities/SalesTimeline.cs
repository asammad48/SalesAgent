using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a single event in the chronological history of a lead's sales journey.
/// This entity serves as an audit log for all interactions.
/// </summary>
public class SalesTimeline
{
    [Key]
    public Guid EventId { get; set; }

    /// <summary>
    * The lead associated with this event.
    /// </summary>
    [ForeignKey(nameof(Lead))]
    public Guid LeadId { get; set; }

    /// <summary>
    /// The type of event that occurred (e.g., 'EmailSent', 'AIResponseReceived', 'StatusChange').
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string EventType { get; set; } = "Generic";

    /// <summary>
    /// The content of the event, such as the body of an email or a system note.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The channel through which the event occurred (e.g., 'Email', 'WhatsApp', 'System').
    /// </summary>
    [MaxLength(50)]
    public string? SourceChannel { get; set; }

    /// <summary>
    /// The actor who initiated the event (e.g., 'AI', 'System', or a human user's name).
    /// </summary>
    [MaxLength(100)]
    public string? Actor { get; set; }

    /// <summary>
    /// The timestamp of when the event occurred.
    /// </summary>
    public DateTime EventTimestamp { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual Lead? Lead { get; set; }
    public virtual AIResponse? AIResponse { get; set; }
}
