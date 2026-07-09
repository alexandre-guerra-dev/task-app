namespace Api.Src.Shared.Exceptions.Base;

public abstract class ConflictException(string message) : Exception(message);