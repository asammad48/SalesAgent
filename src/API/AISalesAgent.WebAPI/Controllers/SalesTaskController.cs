using AISalesAgent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AISalesAgent.WebAPI.Controllers;

[ApiController]
[Route("api/sales-tasks")]
public class SalesTaskController : ControllerBase
{
    private readonly ISalesTaskService _salesTaskService;

    public SalesTaskController(ISalesTaskService salesTaskService)
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
        return Ok(task);
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartTask(Guid id)
    {
        await _salesTaskService.StartTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/pause")]
    public async Task<IActionResult> PauseTask(Guid id)
    {
        await _salesTaskService.PauseTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/resume")]
    public async Task<IActionResult> ResumeTask(Guid id)
    {
        await _salesTaskService.ResumeTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelTask(Guid id)
    {
        await _salesTaskService.CancelTaskAsync(id);
        return NoContent();
    }
}
