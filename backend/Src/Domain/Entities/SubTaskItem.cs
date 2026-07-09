using Api.Src.Domain.Enums;
using Api.Src.Domain.Exceptions;

namespace Api.Src.Domain.Entities;

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

    public SubTaskItem Update(
        string newTitle,
        string? newDescription,
        TaskStatusEnum newStatus,
        DateTime? newDueDate
    )
    {
        SetTitle(newTitle);
        SetDescription(newDescription);
        ChangeStatus(newStatus);
        SetDueDate(newDueDate);

        return this;
    }

    private void SetTitle(string newTitle)
    {
        if (newTitle.Length < 3)
            throw new TaskTitleValidationException();

        Title = newTitle;
    }

    private void SetDescription(string? newDescription)
    {
        Description = newDescription;
    }

    public void ChangeStatus(TaskStatusEnum newStatus)
    {
        Status = newStatus;
        CompletedAt = null;

        if (newStatus == TaskStatusEnum.Complete)
            CompletedAt = DateTime.Now;
    }

    private void SetDueDate(DateTime? newDueDate)
    {
        DueDate = newDueDate;
    }
}