using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Domain.Enums;

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
    // TODO public Guid OwnerId { get; private set; }
    // TODO: Implementar lista de sub tarefas apenas de leitura e métodos de adição, que verifique se a tarefa está concluída e refleta na sub tarefa, e remoção
    public List<SubTaskItem> SubTaskItems { get; private set; } = [];

    public TaskItem(string title, string? description, DateTime? dueDate)
    {
        Id = new();
        Title = title;
        Description = description;
        Status = TaskStatusEnum.Todo;
        CreatedAt = DateTime.Now;
        DueDate = dueDate;
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
        {
            CompletedAt = DateTime.Now;
            foreach (var subTask in SubTaskItems) subTask.ChangeStatus(TaskStatusEnum.Complete);
        }
    }

    public void SetDueDate(DateTime? newDueDate) => DueDate = newDueDate;
}