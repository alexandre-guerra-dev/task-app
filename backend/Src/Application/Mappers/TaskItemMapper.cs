using Api.Src.Api.Dtos.SubTasks;
using Api.Src.Api.Dtos.Tasks;
using Api.Src.Domain.Entities;
using backend.Src.Application.Mappers;

namespace Api.Src.Application.Mappers;

public static class TaskItemMapper
{
    public static TaskItemResponseDto ToResponseDto(this TaskItem model)
    {
        return new
        (
            model.Id,
            model.Title,
            model.Description,
            model.Status,
            model.CreatedAt,
            model.DueDate,
            model.CompletedAt,
            model.Categories.Select(c => c.ToResponseDto()),
            model.SubTaskItems.Select(st => st.ToResponseDto())
        );
    }

    public static SubTaskItemResponseDto ToResponseDto(this SubTaskItem model)
    {
        return new
        (
            model.Id,
            model.Title,
            model.Description,
            model.Status,
            model.CreatedAt,
            model.DueDate,
            model.CompletedAt
        );
    }
}
