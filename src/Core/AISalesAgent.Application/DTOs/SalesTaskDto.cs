using System;

namespace AISalesAgent.Application.DTOs;

public class SalesTaskDto
{
    public Guid TaskId { get; set; }
    public Guid LeadId { get; set; }
    public string TaskType { get; set; } = "Generic";
    public string TaskState { get; set; } = "PENDING";
    public int CurrentStep { get; set; }
    public int TotalSteps { get; set; }
    public DateTime? LastExecutionAt { get; set; }
    public DateTime? NextExecutionAt { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; }
}
