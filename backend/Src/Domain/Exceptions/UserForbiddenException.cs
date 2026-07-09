using Api.Src.Shared.Exceptions.Base;

namespace Api.Src.Domain.Exceptions;

public sealed class UserForbiddenException() : ForbiddenException("User not allowed.");