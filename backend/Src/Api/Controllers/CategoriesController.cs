using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Migrations;
using backend.Src.Api.Dtos.Categories;
using backend.Src.Application.Exceptions;
using backend.Src.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Src.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetAllMy()
    {
        var categories = await _categoryService.GetAllMyAsync();
        return Ok(categories);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto createDto)
    {
        var categories = await _categoryService.CreateAsync(createDto);
        return Ok(categories);
    }

    [Authorize]
    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequestDto updateDto)
    {
        var category = await _categoryService.UpdateAsync(categoryId, updateDto);
        return Ok(category);
    }

    [Authorize]
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid categoryId)
    {
        await _categoryService.DeleteAsync(categoryId);
        return NoContent();
    }
}
