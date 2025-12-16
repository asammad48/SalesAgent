using AISalesAgent.Domain.Entities;
using System;

namespace AISalesAgent.Application.DTOs
{
    public class SalesTaskCreationRequest
    {
        public Guid LeadId { get; set; }
        public string? TaskType { get; set; }
        public DateTime? ScheduledAt { get; set; }
    }
}
