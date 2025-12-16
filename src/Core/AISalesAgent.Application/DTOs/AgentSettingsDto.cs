namespace AISalesAgent.Application.DTOs;

public class AgentSettingsDto
{
    public Guid AgentId { get; set; }
    public string AgentName { get; set; } = "AI Assistant";
    public string AgentPersona { get; set; } = string.Empty;
    public string DefaultTone { get; set; } = "Professional";
    public string DefaultLanguage { get; set; } = "en-US";
    public string TimeZone { get; set; } = "UTC";
}
