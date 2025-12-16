namespace AISalesAgent.Application.DTOs;

public class CtaDto
{
    public Guid CTAId { get; set; }
    public string CTAText { get; set; } = string.Empty;
    public string? CTAType { get; set; }
    public bool IsActive { get; set; }
}
