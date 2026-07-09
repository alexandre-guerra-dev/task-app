using Api.Src.Shared.Exceptions.Base;

namespace Api.Src.Application.Exceptions;

public class SubTaskItemNotFoundException(Guid id) : NotFoundException($"Sub Task {id} Not Found.");