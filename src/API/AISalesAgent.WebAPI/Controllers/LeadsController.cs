using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadsController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLead([FromBody] CreateLeadDto createLeadDto)
    {
        var lead = await _leadService.CreateLeadAsync(createLeadDto);
        return CreatedAtAction(nameof(GetLeadById), new { id = lead.Id }, lead);
    }

    [HttpGet]
    public async Task<IActionResult> GetLeads()
    {
        var leads = await _leadService.GetLeadsAsync();
        return Ok(leads);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeadById(Guid id)
    {
        var lead = await _leadService.GetLeadByIdAsync(id);
        if (lead == null)
        {
            return NotFound();
        }
        return Ok(lead);
    }

    [HttpPut("{id}/stage")]
    public async Task<IActionResult> UpdateLeadStage(Guid id, [FromBody] UpdateLeadStageDto updateLeadStageDto)
    {
        await _leadService.UpdateLeadStageAsync(id, updateLeadStageDto);
        return NoContent();
    }

    [HttpPut("{id}/assign-service")]
    public async Task<IActionResult> AssignServiceToLead(Guid id, [FromBody] AssignServiceDto assignServiceDto)
    {
        await _leadService.AssignServiceToLeadAsync(id, assignServiceDto);
        return NoContent();
    }
}
