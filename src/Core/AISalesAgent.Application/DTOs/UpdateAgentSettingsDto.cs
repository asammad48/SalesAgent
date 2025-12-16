using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class UpdateAgentSettingsDto
{
    [Required]
    [MaxLength(100)]
    public string AgentName { get; set; } = "AI Assistant";

    [Required]
    public string AgentPersona { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string DefaultTone { get; set; } = "Professional";

    [Required]
    [MaxLength(20)]
    public string DefaultLanguage { get; set; } = "en-US";

    [Required]
    [MaxLength(100)]
    public string TimeZone { get; set; } = "UTC";
}
