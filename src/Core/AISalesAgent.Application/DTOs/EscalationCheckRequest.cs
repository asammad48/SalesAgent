using AISalesAgent.Domain.Entities;

namespace AISalesAgent.Application.DTOs
{
    public class EscalationCheckRequest
    {
        public Lead? Lead { get; set; }
        public SalesTask? SalesTask { get; set; }
        public string? Message { get; set; }
    }
}
