using Api.Src.Application.Services;
using Api.Src.Infraestructure.Database;
using Api.Src.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var databasePath = builder.Configuration["Database:Path"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(databasePath));
builder.Services.AddScoped<TaskItemRepository>();

builder.Services.AddScoped<TaskItemService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();