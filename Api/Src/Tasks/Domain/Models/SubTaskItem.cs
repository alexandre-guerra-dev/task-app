using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Tasks.Domain.Enums;

namespace Api.Src.Tasks.Domain.Models;

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
        Status = TaskStatus.Todo;
        CreatedAt = DateTime.Now;
        DueDate = dueDate;
        TaskItemParentId = taskItemParentId;
    }

    public void SetTitle(string newTitle)
    {
        if (newTitle.Length < 3)
            throw new Exception("O novo título possui menos de 3 caracteres.");

        Title = newTitle;
    }

    public void SetDescription(string? newDescription) => Description = newDescription;

    public void ChangeStatus(TaskStatusEnum newStatus)
    {
        Status = newStatus;
        CompletedAt = null;

        if (newStatus == TaskStatusEnum.Complete)
            CompletedAt = DateTime.Now;
    }

    public void SetDueDate(DateTime? newDueDate) => DueDate = newDueDate;
}