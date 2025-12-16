using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Stores the core configuration and persona of the AI sales agent.
/// This entity centralizes all high-level settings for the AI's behavior.
/// </summary>
public class AgentSettings
{
    [Key]
    public Guid AgentId { get; set; }

    /// <summary>
    /// The public-facing name of the AI agent (e.g., "GrowthBot").
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string AgentName { get; set; } = "AI Assistant";

    /// <summary>
    /// A detailed description of the AI's personality, role, and rules.
    /// This is a critical part of the main system prompt.
    /// </summary>
    [Required]
    public string AgentPersona { get; set; } = string.Empty;

    /// <summary>
    /// The default communication tone (e.g., "Professional", "Casual").
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string DefaultTone { get; set; } = "Professional";
    
    /// <summary>
    /// The default language for communication (e.g., 'en-US').
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string DefaultLanguage { get; set; } = "en-US";

    /// <summary>
    /// The operating timezone for the agent, used for scheduling tasks.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string TimeZone { get; set; } = "UTC";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
