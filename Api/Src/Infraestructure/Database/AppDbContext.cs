using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Src.Infraestructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; private set; }
    public DbSet<SubTaskItem> SubTaskItems { get; private set; }

    public AppDbContext(DbContextOptions options) : base(options) {}
}
