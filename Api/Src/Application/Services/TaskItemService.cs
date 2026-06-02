using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Application.Dtos;
using Api.Src.Application.Interfaces;
using Api.Src.Application.Mappers;
using Api.Src.Domain.Models;
using Api.Src.Infraestructure.Repositories;

namespace Api.Src.Application.Services;

public class TaskItemService
{
    private readonly TaskItemRepository _taskItemRepository;
    private readonly IUserContext _userContext;

    public TaskItemService(TaskItemRepository taskItemRepository, IUserContext userContext)
    {
        _taskItemRepository = taskItemRepository;
        _userContext = userContext;
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
        var userId = _userContext.CurrentUserId;

        TaskItem taskItem = new(createDto.Title, createDto.Description, createDto.DueDate, userId);

        await _taskItemRepository.SaveNewTaskItemAsync(taskItem);

        return taskItem.ToResponseDto();
    }

    public async Task<SubTaskItemResponseDto?> CreateSubTaskItemAsync(Guid taskId, CreateTaskItemRequestDto createDto)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            return null;

        SubTaskItem subTaskItem = new(createDto.Title, createDto.Description, createDto.DueDate, taskId);

        taskItem.SubTaskItems.Add(subTaskItem);

        await _taskItemRepository.SaveChangesAsync();

        return subTaskItem.ToResponseDto();
    }

    public async Task<TaskItemResponseDto?> UpdateAsync(Guid taskId, UpdateTaskItemRequestDto updateDto)
    {
        var userId = _userContext.CurrentUserId;

        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            return null;
        
        taskItem.Update(
            updateDto.Title,
            updateDto.Description,
            updateDto.Status,
            updateDto.DueDate,
            userId
        );

        await _taskItemRepository.SaveChangesAsync();

        return taskItem.ToResponseDto();
    }

    public async Task<SubTaskItemResponseDto?> UpdateSubTaskItemAsync(Guid taskId, Guid subTaskId, UpdateTaskItemRequestDto updateDto)
    {
        var userId = _userContext.CurrentUserId;
        
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            return null;

        var subTaskItem = taskItem.UpdateSubTask(
            subTaskId,
            updateDto.Title,
            updateDto.Description,
            updateDto.Status,
            updateDto.DueDate,
            userId
        );

        if (subTaskItem is null)
            return null;

        await _taskItemRepository.SaveChangesAsync();

        return subTaskItem.ToResponseDto();
    }

    public async Task<bool> DeleteAsync(Guid taskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null) 
            return false;

        await _taskItemRepository.DeleteTaskItemAsync(taskItem);
        
        return true;
    }

    public async Task<bool> DeleteSubTaskItemAsync(Guid taskId, Guid subTaskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            return false;

        var subTaskItem = taskItem.SubTaskItems.FirstOrDefault(st => st.Id == subTaskId);

        if (subTaskItem is null)
            return false;

        taskItem.SubTaskItems.Remove(subTaskItem);

        await _taskItemRepository.SaveChangesAsync();

        return true;
    }
}
