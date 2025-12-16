using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AISalesAgent.WebAPI.Controllers;

[ApiController]
[Route("api/ai")]
public class AIControlController : ControllerBase
{
    private readonly IAIControlService _aiControlService;

    public AIControlController(IAIControlService aiControlService)
    {
        _aiControlService = aiControlService;
    }

    [HttpPost("approve-response")]
    public async Task<IActionResult> ApproveResponse([FromBody] ApproveResponseDto approveResponseDto)
    {
        await _aiControlService.ApproveResponseAsync(approveResponseDto);
        return NoContent();
    }

    [HttpPost("reject-response")]
    public async Task<IActionResult> RejectResponse([FromBody] RejectResponseDto rejectResponseDto)
    {
        await _aiControlService.RejectResponseAsync(rejectResponseDto);
        return NoContent();
    }

    [HttpPost("escalate")]
    public async Task<IActionResult> Escalate([FromBody] EscalateDto escalateDto)
    {
        await _aiControlService.EscalateAsync(escalateDto);
        return NoContent();
    }

    [HttpPost("takeover")]
    public async Task<IActionResult> Takeover([FromBody] TakeoverDto takeoverDto)
    {
        await _aiControlService.TakeoverAsync(takeoverDto);
        return NoContent();
    }
}
