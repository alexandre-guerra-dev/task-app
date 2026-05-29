using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Enums;
using Api.Src.Domain.Models;

namespace Api.Src.Application.Dtos;

public record TaskItemResponseDto
(
    Guid Id,
    string Title,
    string? Description,
    TaskStatusEnum Status,
    DateTime CreatedAt,
    DateTime? DueDate,
    DateTime? CompletedAt,
    List<SubTaskItem> SubTaskItems
);