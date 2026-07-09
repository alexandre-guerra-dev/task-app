using Api.Src.Api.Dtos.SubTasks;
using Api.Src.Domain.Enums;

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
    IEnumerable<SubTaskItemResponseDto> SubTaskItems
);