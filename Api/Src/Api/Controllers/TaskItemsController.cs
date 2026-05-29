using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Application.Dtos;
using Api.Src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Src.Api.Controllers;

[ApiController]
[Route("api/task-items")]
public class TaskItemsController : ControllerBase
{
    private readonly TaskItemService _taskItemService;

    public TaskItemsController(TaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var taskItems = await _taskItemService.GetAllAsync();
        return Ok(taskItems);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid taskId)
    {
        var taskItem = await _taskItemService.GetByIdAsync(taskId);

        if (taskItem is null)
            return NotFound("Task Item Not Found.");

        return Ok(taskItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskItemRequestDto createDto)
    {
        var taskItem = await _taskItemService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { taskId = taskItem.Id}, taskItem);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid taskId)
    {
        var succeed = await _taskItemService.DeleteAsync(taskId);

        if (!succeed)
            return NotFound("Task Item Not Found.");
        
        return NoContent();
    }
}
