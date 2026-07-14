using System.Threading.Tasks;
using Api.Src.Api.Dtos.SubTasks;
using Api.Src.Api.Dtos.Tasks;
using Api.Src.Application.Exceptions;
using Api.Src.Application.Interfaces;
using Api.Src.Application.Mappers;
using Api.Src.Domain.Entities;
using Api.Src.Domain.Exceptions;
using Api.Src.Infraestructure.Repositories;
using backend.Src.Api.Dtos.Categories;
using backend.Src.Api.Dtos.Tasks;
using backend.Src.Application.Exceptions;
using backend.Src.Infraestructure.Repositories;

namespace Api.Src.Application.Services;

public class TaskItemService
{
    private readonly TaskItemRepository _taskItemRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly IUserContext _userContext;

    public TaskItemService(
        TaskItemRepository taskItemRepository,
        CategoryRepository categoryRepository,
        IUserContext userContext)
    {
        _taskItemRepository = taskItemRepository;
        _categoryRepository = categoryRepository;
        _userContext = userContext;
    }

    public async Task<TaskItemResponseDto> GetByIdAsync(Guid taskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

        return taskItem.ToResponseDto();
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
    {
        var taskItems = _taskItemRepository.GetAllTaskItems();
        return taskItems.Select(task => task.ToResponseDto());
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetAllMyAsync()
    {
        var taskItems = _taskItemRepository.GetAllOfUser(_userContext.CurrentUserId);
        return taskItems.Select(task => task.ToResponseDto());
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemRequestDto createDto)
    {
        var userId = _userContext.CurrentUserId;

        TaskItem taskItem = new(createDto.Title, createDto.Description, createDto.DueDate, userId);

        await _taskItemRepository.SaveNewTaskItemAsync(taskItem);

        return taskItem.ToResponseDto();
    }

    public async Task<SubTaskItemResponseDto> CreateSubTaskItemAsync(Guid taskId, CreateSubTaskItemRequestDto createDto)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

        if (!taskItem.HasPermission(_userContext.CurrentUserId))
            throw new UserForbiddenException();

        SubTaskItem subTaskItem = new(createDto.Title, createDto.Description, createDto.DueDate, taskId);

        taskItem.SubTaskItems.Add(subTaskItem);

        await _taskItemRepository.SaveChangesAsync();

        return subTaskItem.ToResponseDto();
    }

    public async Task<TaskItemResponseDto> UpdateAsync(Guid taskId, UpdateTaskItemRequestDto updateDto)
    {
        var userId = _userContext.CurrentUserId;

        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

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

    public async Task<SubTaskItemResponseDto> UpdateSubTaskItemAsync(Guid taskId, Guid subTaskId, UpdateSubTaskItemRequestDto updateDto)
    {
        var userId = _userContext.CurrentUserId;

        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

        var subTaskItem = taskItem.UpdateSubTask(
            subTaskId,
            updateDto.Title,
            updateDto.Description,
            updateDto.Status,
            updateDto.DueDate,
            userId
        );

        if (subTaskItem is null)
            throw new SubTaskItemNotFoundException(taskId);

        await _taskItemRepository.SaveChangesAsync();

        return subTaskItem.ToResponseDto();
    }

    public async Task DeleteAsync(Guid taskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

        if (!taskItem.HasPermission(_userContext.CurrentUserId))
            throw new UserForbiddenException();

        await _taskItemRepository.DeleteTaskItemAsync(taskItem);
    }

    public async Task DeleteSubTaskItemAsync(Guid taskId, Guid subTaskId)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

        var subTaskItem = taskItem.SubTaskItems.FirstOrDefault(st => st.Id == subTaskId);

        if (subTaskItem is null)
            throw new SubTaskItemNotFoundException(subTaskId);

        if (!taskItem.HasPermission(_userContext.CurrentUserId))
            throw new UserForbiddenException();

        taskItem.SubTaskItems.Remove(subTaskItem);

        await _taskItemRepository.SaveChangesAsync();
    }

    public async Task<TaskItemResponseDto> UpdateCategoriesAsync(Guid taskId, UpdateCategoriesRequestDto updateDto)
    {
        var taskItem = await _taskItemRepository.GetTaskItemByIdAsync(taskId);

        if (taskItem is null)
            throw new TaskItemNotFoundException(taskId);

        var categories = await _categoryRepository.GetByIdAsync(updateDto.categoryIds);

        if (categories.Count() != updateDto.categoryIds.Count)
            throw new SomeCategoryNotFoundException();

        taskItem.UpdateCategories(_userContext.CurrentUserId, categories);

        await _taskItemRepository.SaveChangesAsync();

        return taskItem.ToResponseDto();
    }
}
