using Api.Src.Api.Dtos.SubTasks;
using Api.Src.Api.Dtos.Tasks;
using Api.Src.Application.Services;
using backend.Src.Api.Dtos.Tasks;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetAllMy()
    {
        var taskItems = await _taskItemService.GetAllMyAsync();
        return Ok(taskItems);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid taskId)
    {
        var taskItem = await _taskItemService.GetByIdAsync(taskId);
        return Ok(taskItem);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskItemRequestDto createDto)
    {
        var taskItem = await _taskItemService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { taskId = taskItem.Id}, taskItem);
    }

    [Authorize]
    [HttpPost("{taskId:guid}/sub-task-items")]
    public async Task<IActionResult> CreateSubTaskItem([FromRoute] Guid taskId, [FromBody] CreateSubTaskItemRequestDto createDto)
    {
        var subTaskItem = await _taskItemService.CreateSubTaskItemAsync(taskId, createDto);
        return Ok(subTaskItem);
    }

    [Authorize]
    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] UpdateTaskItemRequestDto updateDto)
    {
        var task = await _taskItemService.UpdateAsync(taskId, updateDto);
        return Ok(task);
    }

    [Authorize]
    [HttpPut("{taskId:guid}/sub-task-items/{subTaskId:guid}")]
    public async Task<IActionResult> UpdateSubTaskItem(
        [FromRoute] Guid taskId,
        [FromRoute] Guid subTaskId,
        [FromBody] UpdateSubTaskItemRequestDto updateDto
    )
    {
        var subTask = await _taskItemService.UpdateSubTaskItemAsync(taskId, subTaskId, updateDto);
        return Ok(subTask);
    }

    [Authorize]
    [HttpPut("{taskId:guid}/categories")]
    public async Task<IActionResult> UpdateCategories([FromRoute] Guid taskId, [FromBody] UpdateCategoriesRequestDto updateDto)
    {
        var task = await _taskItemService.UpdateCategoriesAsync(taskId, updateDto);
        return Ok(task);
    }

    [Authorize]
    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid taskId)
    {
        await _taskItemService.DeleteAsync(taskId);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{taskId:guid}/sub-task-items/{subTaskId:guid}")]
    public async Task<IActionResult> DeleteSubTaskItem([FromRoute] Guid taskId, [FromRoute] Guid subTaskId)
    {
        await _taskItemService.DeleteSubTaskItemAsync(taskId, subTaskId);
        return NoContent();
    }
}
