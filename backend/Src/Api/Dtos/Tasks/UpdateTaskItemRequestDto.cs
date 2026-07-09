using System.ComponentModel.DataAnnotations;
using Api.Src.Domain.Enums;

namespace Api.Src.Api.Dtos.Tasks;

public record UpdateTaskItemRequestDto
(
    [Required]
    [MinLength(3)]
    string Title,

    string? Description,

    [Required]
    TaskStatusEnum Status,
    
    DateTime? DueDate
);