using Api.Src.Api.Dtos.SubTasks;
using Api.Src.Domain.Enums;
using backend.Src.Api.Dtos.Categories;
using backend.Src.Domain.Entities;

namespace Api.Src.Api.Dtos.Tasks;

public record TaskItemResponseDto
(
    Guid Id,
    string Title,
    string? Description,
    TaskStatusEnum Status,
    DateTime CreatedAt,
    DateTime? DueDate,
    DateTime? CompletedAt,
    IEnumerable<CategoryResponseDto> Categories,
    IEnumerable<SubTaskItemResponseDto> SubTaskItems
);