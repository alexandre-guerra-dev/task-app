using System.ComponentModel.DataAnnotations;

namespace Api.Src.Api.Dtos.SubTasks;

public record CreateSubTaskItemRequestDto
(
    [Required]
    [MinLength(3)]
    string Title,

    string? Description,
    
    DateTime? DueDate
);