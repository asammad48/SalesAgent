using System;
using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class ApproveResponseDto
{
    [Required]
    public Guid ResponseId { get; set; }
    public string? Notes { get; set; }
}

public class RejectResponseDto
{
    [Required]
    public Guid ResponseId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;
}

public class EscalateDto
{
    [Required]
    public Guid LeadId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Notes { get; set; }
}

public class TakeoverDto
{
    [Required]
    public Guid LeadId { get; set; }

    [Required]
    [MaxLength(100)]
    public string HumanAgentId { get; set; } = string.Empty;
}
