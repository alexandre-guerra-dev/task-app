using Api.Src.Shared.Exceptions.Base;

namespace Api.Src.Domain.Exceptions;

public class TaskTitleValidationException() : ValidationException("The title length must be greather than 2 characters.");