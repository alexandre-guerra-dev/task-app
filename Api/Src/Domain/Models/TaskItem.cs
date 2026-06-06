using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Enums;
using Api.Src.Domain.Exceptions;
using Api.Src.Infraestructure.Identity;

namespace Api.Src.Domain.Models;

public class TaskItem
{
    // Properties
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskStatusEnum Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    // Navigation Properties
    public Guid OwnerId { get; private set; }
    public AppUser? Owner { get; private set; }

    // TODO: Implementar lista de sub tarefas apenas de leitura e métodos de adição, que verifique se a tarefa está concluída e refleta na sub tarefa, e remoção
    public List<SubTaskItem> SubTaskItems { get; private set; } = [];

    public TaskItem(string title, string? description, DateTime? dueDate, Guid ownerId)
    {
        Id = new();
        Title = title;
        Description = description;
        Status = TaskStatusEnum.Todo;
        CreatedAt = DateTime.Now;
        DueDate = dueDate;
        OwnerId = ownerId;
    }

    public TaskItem Update(
        string newTitle,
        string? newDescription,
        TaskStatusEnum newStatus,
        DateTime? newDueDate,
        Guid userId
    )
    {
        if (!IsAuthorized(userId))
            throw new UserUnauthorizedException();

        SetTitle(newTitle);
        SetDescription(newDescription);
        ChangeStatus(newStatus);
        SetDueDate(newDueDate);

        return this;
    }

    public SubTaskItem? UpdateSubTask(
        Guid subTaskId,
        string newTitle,
        string? newDescription,
        TaskStatusEnum newStatus,
        DateTime? newDueDate,
        Guid userId
    )
    {
        if (!IsAuthorized(userId))
            throw new UserUnauthorizedException();

        var subTask = SubTaskItems.FirstOrDefault(st => st.Id == subTaskId);

        if (subTask is null)
            return null;

        subTask.Update(
            newTitle,
            newDescription,
            newStatus,
            newDueDate
        );

        return subTask;
    }

    private void ChangeStatus(TaskStatusEnum newStatus)
    {
        Status = newStatus;
        CompletedAt = null;

        if (newStatus == TaskStatusEnum.Complete)
        {
            CompletedAt = DateTime.Now;
            foreach (var subTask in SubTaskItems) subTask.ChangeStatus(TaskStatusEnum.Complete);
        }
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


    private void SetDueDate(DateTime? newDueDate)
    {
        DueDate = newDueDate;
    }

    public bool IsAuthorized(Guid userId) => userId == OwnerId;
}