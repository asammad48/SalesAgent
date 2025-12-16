using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class CreateServiceDto
{
    [Required]
    [MaxLength(200)]
    public string ServiceName { get; set; } = string.Empty;

    [Required]
    public string ServiceDescription { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
