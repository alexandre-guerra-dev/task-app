using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Application.Interfaces;
using Api.Src.Domain.Exceptions;
using backend.Src.Api.Dtos.Categories;
using backend.Src.Application.Exceptions;
using backend.Src.Application.Mappers;
using backend.Src.Domain.Entities;
using backend.Src.Domain.Exceptions;
using backend.Src.Infraestructure.Repositories;

namespace backend.Src.Application.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;
    private readonly IUserContext _userContext;

    public CategoryService(CategoryRepository categoryRepository, IUserContext userContext)
    {
        _categoryRepository = categoryRepository;
        _userContext = userContext;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => c.ToResponseDto());
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllMyAsync()
    {
        var userId = _userContext.CurrentUserId;

        var categories = await _categoryRepository.GetAllOfUserAsync(userId);
        
        return categories.Select(c => c.ToResponseDto());
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryRequestDto createDto)
    {
        var userId = _userContext.CurrentUserId;

        var categories = await _categoryRepository.GetAllOfUserAsync(userId);

        if (categories.Any(c => c.Name == createDto.Name))
            throw new CategoryAlreadyExistsException(createDto.Name);

        Category category = new(createDto.Name, userId);

        await _categoryRepository.SaveNewAsync(category);

        return category.ToResponseDto();
    }

    public async Task<CategoryResponseDto> UpdateAsync(Guid categoryId, UpdateCategoryRequestDto updateDto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category is null)
            throw new CategoryNotFoundException(categoryId);

        var categories = await _categoryRepository.GetAllOfUserAsync(_userContext.CurrentUserId);

        if (categories.Any(c => c.Name == updateDto.Name))
            throw new CategoryAlreadyExistsException(updateDto.Name);

        category.Update(updateDto.Name, _userContext.CurrentUserId);

        await _categoryRepository.SaveChangesAsync();

        return category.ToResponseDto();
    }

    public async Task DeleteAsync(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category is null)
            throw new CategoryNotFoundException(categoryId);

        if (!category.HasPermission(_userContext.CurrentUserId))
            throw new UserForbiddenException();

        await _categoryRepository.DeleteAsync(category);
    }
}
