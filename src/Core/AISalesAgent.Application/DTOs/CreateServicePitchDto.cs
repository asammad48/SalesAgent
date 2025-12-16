using System;
using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

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
