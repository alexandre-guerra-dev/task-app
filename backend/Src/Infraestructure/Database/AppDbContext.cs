using Api.Src.Domain.Entities;
using Api.Src.Infraestructure.Identity;
using backend.Src.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Src.Infraestructure.Database;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public DbSet<TaskItem> TaskItems { get; private set; }
    public DbSet<SubTaskItem> SubTaskItems { get; private set; }
    public DbSet<Category> Categories { get; private set; }

    public AppDbContext(DbContextOptions options) : base(options) {}
}
