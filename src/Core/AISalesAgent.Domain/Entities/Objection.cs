using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a common sales objection and the approved response.
/// This allows the founder to train the AI on handling pushback.
/// </summary>
public class Objection
{
    [Key]
    public Guid ObjectionId { get; set; }

    /// <summary>
    // A short name for the objection for easy identification (e.g., "Too Expensive").
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string ObjectionName { get; set; } = string.Empty;

    /// <summary>
    /// Keywords or phrases that might indicate this objection is being raised.
    /// This can be used for more advanced matching logic in the future.
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// The founder-approved response to this objection.
    /// This text will be provided to the AI as a primary resource.
    /// </summary>
    [Required]
    public string ResponseText { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
