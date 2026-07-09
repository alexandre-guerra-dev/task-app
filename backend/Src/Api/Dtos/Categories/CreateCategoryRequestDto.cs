using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Src.Application.Exceptions;

public record CreateCategoryRequestDto(
    [Required]
    [MinLength(3)]
    string Name
);
