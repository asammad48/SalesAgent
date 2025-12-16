using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class EscalateDto
{
    public Guid LeadId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Notes { get; set; }
}
