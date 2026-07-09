using System.Security.Claims;
using Api.Src.Api.Exceptions;
using Api.Src.Application.Interfaces;

namespace Api.Src.Api.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid CurrentUserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userId, out var userGuid))
                return userGuid;

            throw new UserUnauthorizedException();
        }
    }
}
