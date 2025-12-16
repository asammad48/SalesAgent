using System.Collections.Generic;

namespace AISalesAgent.Application.DTOs;

public class LeadInputDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string? ServiceInterest { get; set; }
    public Dictionary<string, string>? AdditionalFields { get; set; }
}
