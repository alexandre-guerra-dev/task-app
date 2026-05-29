using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Src.Application.Dtos;

public record CreateTaskItemRequestDto
(
    [Required]
    [MinLength(3)]
    string Title,

    string? Description,
    
    DateTime? DueDate
);