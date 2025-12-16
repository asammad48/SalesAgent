using AISalesAgent.Domain.Enums;
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
    /// The time at which the task is scheduled to be executed.
    /// </summary>
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// The time at which the task was completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// The time for the next action if the task is in a waiting state.
    /// </summary>
    public DateTime? NextActionAt { get; set; }

    /// <summary>
    /// A reason for task failure, if any.
    /// </summary>
    public string? FailureReason { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Lead? Lead { get; set; }
}
