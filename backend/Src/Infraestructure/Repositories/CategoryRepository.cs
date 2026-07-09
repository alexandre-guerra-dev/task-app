using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Infraestructure.Database;
using backend.Src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Src.Infraestructure.Repositories;

public class CategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId)
    {
        return await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }
    
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = _dbContext.Categories.AsNoTracking();
        return categories;
    }

    public async Task<IEnumerable<Category>> GetAllMyAsync(Guid ownerId)
    {
        var categories = _dbContext.Categories
            .Where(c => c.OwnerId == ownerId)
            .AsNoTracking();
            
        return categories;
    }

    public async Task SaveNewAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Category category)
    {
        _dbContext.Categories.Remove(category);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
