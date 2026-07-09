namespace Api.Src.Shared.Exceptions.Base;

public abstract class ValidationException(string message) : Exception(message);