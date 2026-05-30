using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Enums;

namespace Api.Src.Application.Dtos;

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