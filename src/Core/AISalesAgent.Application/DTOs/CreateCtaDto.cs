using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class CreateCtaDto
{
    [Required]
    [MaxLength(500)]
    public string CTAText { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? CTAType { get; set; }

    public bool IsActive { get; set; } = true;
}
