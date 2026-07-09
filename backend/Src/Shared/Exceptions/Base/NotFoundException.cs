namespace Api.Src.Shared.Exceptions.Base;

public abstract class NotFoundException(string message) : Exception(message);