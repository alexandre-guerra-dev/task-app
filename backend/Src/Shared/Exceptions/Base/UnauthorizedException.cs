namespace Api.Src.Shared.Exceptions.Base;

public abstract class UnauthorizedException(string message) : Exception(message);