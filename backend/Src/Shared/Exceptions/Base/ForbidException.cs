namespace Api.Src.Shared.Exceptions.Base;

public abstract class ForbiddenException(string message) : Exception(message);