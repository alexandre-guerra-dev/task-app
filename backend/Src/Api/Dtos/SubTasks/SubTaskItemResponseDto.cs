using Api.Src.Domain.Enums;

namespace Api.Src.Api.Dtos.SubTasks;

public record SubTaskItemResponseDto
(
    Guid Id,
    string Title,
    string? Description,
    TaskStatusEnum Status,
    DateTime CreatedAt,
    DateTime? DueDate,
    DateTime? CompletedAt
);