using AISalesAgent.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AISalesAgent.Domain.Entities;

/// <summary>
/// Represents a background task related to the sales process for a specific lead.
/// This is used to manage the state of long-running, asynchronous operations.
/// </summary>
public class SalesTask
{
    [Key]
    public Guid TaskId { get; set; }

    /// <summary>
    /// The specific lead this task is for.
    /// </summary>
    [ForeignKey(nameof(Lead))]
    public Guid LeadId { get; set; }

    /// <summary>
    /// The type of task (e.g., 'InitialOutreach', 'FollowUp', 'NurturingSequence').
    /// </summary>
    [Required]
    public string TaskType { get; set; } = "Generic";

    /// <summary>
    /// The current state of the task (e.g., 'PENDING', 'RUNNING', 'COMPLETED').
    /// </summary>
    [Required]
    public TaskState TaskState { get; set; } = TaskState.PENDING;

    /// <summary>
    /// The current step in a multi-step workflow.
    /// </summary>
    public int CurrentStep { get; set; } = 0;

    /// <summary>
    /// The total number of steps in the workflow.
    /// </summary>
    public int TotalSteps { get; set; } = 1;

    /// <summary>
    /// The time of the last execution attempt.
    /// </summary>
    public DateTime? LastExecutionAt { get; set; }

    /// <summary>
    /// The time for the next scheduled execution.
    /// </summary>
    public DateTime? NextExecutionAt { get; set; }

    /// <summary>
    /// A reason for task failure, if any.
    /// </summary>
    public string? FailureReason { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Lead? Lead { get; set; }

    /// <summary>
    /// Detailed logs of each execution step.
    /// </summary>
    public virtual ICollection<ExecutionLog> ExecutionLogs { get; set; } = new List<ExecutionLog>();
}
