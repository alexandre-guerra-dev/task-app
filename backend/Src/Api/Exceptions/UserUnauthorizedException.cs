using Api.Src.Shared.Exceptions.Base;

namespace Api.Src.Api.Exceptions;

public class UserUnauthorizedException() : UnauthorizedException("User Not Authorized.");
