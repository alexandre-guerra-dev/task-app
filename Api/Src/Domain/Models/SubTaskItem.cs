using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Enums;

namespace Api.Src.Domain.Models;

public class SubTaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskStatusEnum Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public Guid TaskItemParentId { get; private set; }
    public TaskItem? TaskItemParent { get; private set; }

    public SubTaskItem(string title, string? description, DateTime? dueDate, Guid taskItemParentId)
    {
        Id = new();
        Title = title;
        Description = description;
        Status = TaskStatusEnum.Todo;
        CreatedAt = DateTime.Now;
        DueDate = dueDate;
        TaskItemParentId = taskItemParentId;
    }

    public bool Update(
        string newTitle,
        string? newDescription,
        TaskStatusEnum newStatus,
        DateTime? newDueDate
    )
    {
        if (!SetTitle(newTitle))
            return false;
        
        SetDescription(newDescription);
        ChangeStatus(newStatus);
        SetDueDate(newDueDate);

        return true;
    }

    private bool SetTitle(string newTitle)
    {
        if (newTitle.Length < 3)
            return false;

        Title = newTitle;

        return true;
    }

    private bool SetDescription(string? newDescription)
    {
        Description = newDescription;

        return true;
    }

    public bool ChangeStatus(TaskStatusEnum newStatus)
    {
        Status = newStatus;
        CompletedAt = null;

        if (newStatus == TaskStatusEnum.Complete)
            CompletedAt = DateTime.Now;

        return true;
    }

    private bool SetDueDate(DateTime? newDueDate)
    {
        DueDate = newDueDate;

        return true;
    }
}