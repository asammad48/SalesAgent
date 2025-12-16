using System;
using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class ServiceDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class CreateServiceDto
{
    [Required]
    [MaxLength(200)]
    public string ServiceName { get; set; } = string.Empty;

    [Required]
    public string ServiceDescription { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}

public class ServicePitchDto
{
    public Guid PitchId { get; set; }
    public Guid ServiceId { get; set; }
    public string PitchType { get; set; } = "Generic";
    public string PitchTitle { get; set; } = string.Empty;
    public string PitchText { get; set; } = string.Empty;
}

public class CreateServicePitchDto
{
    [Required]
    public Guid ServiceId { get; set; }

    [Required]
    [MaxLength(50)]
    public string PitchType { get; set; } = "Generic";

    [Required]
    [MaxLength(200)]
    public string PitchTitle { get; set; } = string.Empty;

    [Required]
    public string PitchText { get; set; } = string.Empty;
}

public class CtaDto
{
    public Guid CTAId { get; set; }
    public string CTAText { get; set; } = string.Empty;
    public string? CTAType { get; set; }
    public bool IsActive { get; set; }
}

public class CreateCtaDto
{
    [Required]
    [MaxLength(500)]
    public string CTAText { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? CTAType { get; set; }

    public bool IsActive { get; set; } = true;
}

public class AgentSettingsDto
{
    public Guid AgentId { get; set; }
    public string AgentName { get; set; } = "AI Assistant";
    public string AgentPersona { get; set; } = string.Empty;
    public string DefaultTone { get; set; } = "Professional";
    public string DefaultLanguage { get; set; } = "en-US";
    public string TimeZone { get; set; } = "UTC";
}

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
