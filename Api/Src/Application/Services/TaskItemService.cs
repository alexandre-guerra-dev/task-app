using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Application.Dtos;
using Api.Src.Application.Mappers;
using Api.Src.Domain.Models;
using Api.Src.Infraestructure.Repositories;

namespace Api.Src.Application.Services;

public class TaskItemService
{
    private readonly TaskItemRepository _taskItemRepository;

    public TaskItemService(TaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<TaskItemResponseDto?> GetByIdAsync(Guid taskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);
        return taskItem?.ToResponseDto();
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
    {
        var taskItems = _taskItemRepository.GetAllTaskItems();
        return taskItems.Select(task => task.ToResponseDto());
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemRequestDto createDto)
    {
        TaskItem taskItem = new(createDto.Title, createDto.Description, createDto.DueDate);

        await _taskItemRepository.SaveNewTaskItemAsync(taskItem);

        return taskItem.ToResponseDto();
    }

    public async Task<bool> DeleteAsync(Guid taskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null) 
            return false;

        await _taskItemRepository.DeleteTaskItemAsync(taskItem);
        
        return true;
    }
}
