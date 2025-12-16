using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class RejectResponseDto
{
    public Guid ResponseId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;
}
