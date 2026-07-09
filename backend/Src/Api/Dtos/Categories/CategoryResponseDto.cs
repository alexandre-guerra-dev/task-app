using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Src.Api.Dtos.Categories;

public record CategoryResponseDto(
    Guid Id,
    string Name
);
