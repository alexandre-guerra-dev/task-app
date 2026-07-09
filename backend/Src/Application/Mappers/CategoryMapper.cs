using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Src.Api.Dtos.Categories;
using backend.Src.Domain.Entities;

namespace backend.Src.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryResponseDto ToResponseDto(this Category category) => new(category.Id, category.Name);
}
