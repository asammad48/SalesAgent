using AISalesAgent.Domain.Entities;
using AISalesAgent.Domain.Enums;

namespace AISalesAgent.Application.DTOs
{
    public class StageTransitionRequest
    {
        public Lead? Lead { get; set; }
        public SalesStage NewStage { get; set; }
    }
}
