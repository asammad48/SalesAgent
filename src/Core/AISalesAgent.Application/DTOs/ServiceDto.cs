namespace AISalesAgent.Application.DTOs;

public class ServiceDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
