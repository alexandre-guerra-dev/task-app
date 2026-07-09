using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Entities;
using Api.Src.Infraestructure.Identity;

namespace backend.Src.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = "";

    public Guid OwnerId { get; private set; }
    public AppUser? Owner { get; private set; }

    public List<TaskItem> TaskItems { get; set; } = [];
}
