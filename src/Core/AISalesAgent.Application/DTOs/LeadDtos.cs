using System;
using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Application.DTOs;

public class LeadDto
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateLeadDto
{
    [Required]
    [MaxLength(200)]
    public string CompanyName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? ContactPerson { get; set; }

    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Phone]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
}

public class UpdateLeadStageDto
{
    [Required]
    public string NewStage { get; set; } = string.Empty;
}

public class AssignServiceDto
{
    [Required]
    public Guid ServiceId { get; set; }
}
