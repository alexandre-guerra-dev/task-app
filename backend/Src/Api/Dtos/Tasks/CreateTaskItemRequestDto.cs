using System.ComponentModel.DataAnnotations;

namespace Api.Src.Api.Dtos.Tasks;

public record CreateTaskItemRequestDto
(
    [Required]
    [MinLength(3)]
    string Title,

    string? Description,
    
    DateTime? DueDate
);