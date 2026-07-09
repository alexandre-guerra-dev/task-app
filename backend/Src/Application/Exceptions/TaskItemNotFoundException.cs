using Api.Src.Shared.Exceptions.Base;

namespace Api.Src.Application.Exceptions;

public class TaskItemNotFoundException(Guid id) : NotFoundException($"Task {id} Not Found.");