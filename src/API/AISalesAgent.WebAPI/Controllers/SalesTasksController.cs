using AISalesAgent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AISalesAgent.WebAPI.Controllers;

[ApiController]
[Route("api/sales-tasks")]
public class SalesTasksController : ControllerBase
{
    private readonly ISalesTaskService _salesTaskService;

    public SalesTasksController(ISalesTaskService salesTaskService)
    {
        _salesTaskService = salesTaskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSalesTasks()
    {
        var tasks = await _salesTaskService.GetSalesTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSalesTaskById(Guid id)
    {
        var task = await _salesTaskService.GetSalesTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartTask(Guid id)
    {
        await _salesTaskService.StartTaskAsync(id);
        return Ok();
    }

    [HttpPost("{id}/pause")]
    public async Task<IActionResult> PauseTask(Guid id)
    {
        await _salesTaskService.PauseTaskAsync(id);
        return Ok();
    }

    [HttpPost("{id}/resume")]
    public async Task<IActionResult> ResumeTask(Guid id)
    {
        await _salesTaskService.ResumeTaskAsync(id);
        return Ok();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelTask(Guid id)
    {
        await _salesTaskService.CancelTaskAsync(id);
        return Ok();
    }
}
