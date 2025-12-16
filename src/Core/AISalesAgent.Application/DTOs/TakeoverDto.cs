using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class TakeoverDto
{
    public Guid LeadId { get; set; }

    [Required]
    [MaxLength(100)]
    public string HumanAgentId { get; set; } = string.Empty;
}
