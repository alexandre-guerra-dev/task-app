using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Src.Api.Dtos.Tasks;

public record UpdateCategoriesRequestDto(
    [Required]
    HashSet<Guid> categoryIds
);
