namespace AISalesAgent.Application.Models
{
    public class PromptCompilationResult
    {
        public string FinalPrompt { get; set; } = string.Empty;
        public PromptMetadata Metadata { get; set; } = new PromptMetadata();
    }

    public class PromptMetadata
    {
        public string? SalesStage { get; set; }
        public string? Intent { get; set; }
        public bool IsEscalationRisk { get; set; }
    }
}
