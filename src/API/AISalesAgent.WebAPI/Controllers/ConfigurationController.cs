using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AISalesAgent.WebAPI.Controllers;

[ApiController]
[Route("api")]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationService _configurationService;

    public ConfigurationController(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet("services")]
    public async Task<IActionResult> GetServices()
    {
        var services = await _configurationService.GetServicesAsync();
        return Ok(services);
    }

    [HttpPost("services")]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceDto createServiceDto)
    {
        var service = await _configurationService.CreateServiceAsync(createServiceDto);
        return CreatedAtAction(nameof(GetServices), new { id = service.ServiceId }, service);
    }

    [HttpGet("pitches")]
    public async Task<IActionResult> GetPitches()
    {
        var pitches = await _configurationService.GetPitchesAsync();
        return Ok(pitches);
    }

    [HttpPost("pitches")]
    public async Task<IActionResult> CreatePitch([FromBody] CreateServicePitchDto createServicePitchDto)
    {
        var pitch = await _configurationService.CreatePitchAsync(createServicePitchDto);
        return CreatedAtAction(nameof(GetPitches), new { id = pitch.PitchId }, pitch);
    }

    [HttpGet("ctas")]
    public async Task<IActionResult> GetCtas()
    {
        var ctas = await _configurationService.GetCtasAsync();
        return Ok(ctas);
    }

    [HttpPost("ctas")]
    public async Task<IActionResult> CreateCta([FromBody] CreateCtaDto createCtaDto)
    {
        var cta = await _configurationService.CreateCtaAsync(createCtaDto);
        return CreatedAtAction(nameof(GetCtas), new { id = cta.CTAId }, cta);
    }

    [HttpGet("agent-settings")]
    public async Task<IActionResult> GetAgentSettings()
    {
        var settings = await _configurationService.GetAgentSettingsAsync();
        return Ok(settings);
    }

    [HttpPut("agent-settings")]
    public async Task<IActionResult> UpdateAgentSettings([FromBody] UpdateAgentSettingsDto updateAgentSettingsDto)
    {
        await _configurationService.UpdateAgentSettingsAsync(updateAgentSettingsDto);
        return NoContent();
    }
}
