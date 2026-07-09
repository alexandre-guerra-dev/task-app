using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Src.Api.Dtos.Categories;

public record UpdateCategoryRequestDto(
    [Required]
    [MinLength(3)]
    string Name
);
