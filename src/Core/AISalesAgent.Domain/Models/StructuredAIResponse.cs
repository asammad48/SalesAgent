using System.Collections.Generic;

namespace AISalesAgent.Domain.Models
{
    public class StructuredAIResponse
    {
        public string ResponseText { get; set; } = string.Empty;
        public bool IsEscalationRecommended { get; set; }
        public string SuggestedNextAction { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
