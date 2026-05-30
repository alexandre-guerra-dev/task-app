using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Application.Dtos;
using Api.Src.Domain.Models;

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
