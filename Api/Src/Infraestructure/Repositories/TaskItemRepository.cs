using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Models;
using Api.Src.Infraestructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Src.Infraestructure.Repositories;

public class TaskItemRepository
{
    private AppDbContext _dbContext;
    public TaskItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<TaskItem> GetAllTaskItems()
    {
        return _dbContext.TaskItems
            .Include(task => task.SubTaskItems)
            .AsNoTracking();
    }

    public async Task<TaskItem?> GetTaskItemByIdAsync(Guid taskId)
    {
        return await _dbContext.TaskItems
            .Include(task => task.SubTaskItems)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task SaveNewTaskItemAsync(TaskItem taskItem)
    {
        await _dbContext.TaskItems.AddAsync(taskItem);

        foreach (var subTask in taskItem.SubTaskItems)
        {
            await _dbContext.SubTaskItems.AddAsync(subTask);
        }

        await SaveChangesAsync();
    }

    public async Task DeleteTaskItemAsync(TaskItem taskItem)
    {
        _dbContext.TaskItems.Remove(taskItem);

        foreach (var subTask in taskItem.SubTaskItems)
        {
            _dbContext.SubTaskItems.Remove(subTask);
        }

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
