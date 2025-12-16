using AISalesAgent.Domain.Entities;

namespace AISalesAgent.Application.DTOs
{
    public class PromptContextDto
    {
        public Lead? Lead { get; set; }
        public string? SalesStage { get; set; }
        public Service? Service { get; set; }
        public ServicePitch? ServicePitch { get; set; }
        public CTA? CTA { get; set; }
        public Objection? Objection { get; set; }
        public AgentSettings? AgentSettings { get; set; }
    }
}
