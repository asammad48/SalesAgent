using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class UpdateLeadStageDto
{
    [Required]
    public string NewStage { get; set; } = string.Empty;
}
