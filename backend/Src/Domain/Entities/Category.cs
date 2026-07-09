using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Api.Exceptions;
using Api.Src.Domain.Entities;
using Api.Src.Domain.Exceptions;
using Api.Src.Infraestructure.Identity;
using Api.Src.Shared.Exceptions.Base;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace backend.Src.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = "";

    public Guid OwnerId { get; private set; }
    public AppUser? Owner { get; private set; }

    public List<TaskItem> TaskItems { get; set; } = [];

    public Category(string name, Guid ownerId)
    {
        Id = new();
        Name = name;
        OwnerId = ownerId;
    }

    public Category Update(string newName, Guid userId)
    {
        if (!HasPermission(userId))
            throw new UserForbiddenException();
        Name = newName;

        return this;
    }

    public bool HasPermission(Guid userId) => OwnerId == userId;
}
