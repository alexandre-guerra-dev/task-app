namespace Api.Src.Shared.Exceptions.Base;

public abstract class BusinessException(string message) : Exception(message);