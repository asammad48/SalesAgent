using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a product or service that the AI agent can sell.
/// </summary>
public class Service
{
    [Key]
    public Guid ServiceId { get; set; }

    /// <summary>
    /// The name of the service.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// A detailed description of the service.
    /// </summary>
    [Required]
    public string ServiceDescription { get; set; } = string.Empty;

    /// <summary>
    /// Whether the service is currently offered.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for all the different types of pitches associated with this service
    public virtual ICollection<ServicePitch> Pitches { get; set; } = new List<ServicePitch>();
}
