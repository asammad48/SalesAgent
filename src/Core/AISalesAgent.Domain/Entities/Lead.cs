using AISalesAgent.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a potential customer or lead. This is a central entity in the sales process.
/// </summary>
public class Lead
{
    /// <summary>
    /// The unique identifier for the lead.
    /// </summary>
    [Key]
    public Guid LeadId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    public string? CompanyName { get; set; }

    /// <summary>
    /// The source from which the lead was acquired (e.g., 'Website Form', 'Manual Import').
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// The overall status of the lead (e.g., 'New', 'Contacted', 'Qualified', 'Unqualified').
    /// </summary>
    [Required]
    public string Status { get; set; } = "New";

    /// <summary>
    /// The specific stage in the sales pipeline (e.g., 'InitialContact', 'NeedsAnalysis').
    /// </summary>
    [Required]
    public SalesStage SalesStage { get; set; } = SalesStage.NEW;

    /// <summary>
    /// A score indicating the quality or potential value of the lead.
    /// </summary>
    public int LeadScore { get; set; }

    /// <summary>
    /// The user or system currently responsible for the lead (e.g., 'AI' or a human user's ID).
    /// </summary>
    public string AssignedTo { get; set; } = "AI";

    public Guid? ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public virtual Service? Service { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties for related data
    public virtual ICollection<SalesTask> SalesTasks { get; set; } = new List<SalesTask>();
    public virtual ICollection<SalesTimeline> TimelineEvents { get; set; } = new List<SalesTimeline>();
}
