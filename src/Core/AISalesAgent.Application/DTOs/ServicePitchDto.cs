namespace AISalesAgent.Application.DTOs;

public class ServicePitchDto
{
    public Guid PitchId { get; set; }
    public Guid ServiceId { get; set; }
    public string PitchType { get; set; } = "Generic";
    public string PitchTitle { get; set; } = string.Empty;
    public string PitchText { get; set; } = string.Empty;
}
