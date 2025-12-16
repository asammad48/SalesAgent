using System.ComponentModel.DataAnnotations;

namespace AISalesAgent.Domain.Entities;

public class ExecutionLog
{
    [Key]
    public Guid Id { get; set; }
    public Guid SalesTaskId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;

    public virtual SalesTask? SalesTask { get; set; }
}
