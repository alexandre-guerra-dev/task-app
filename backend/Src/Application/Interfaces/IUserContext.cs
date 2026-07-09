namespace Api.Src.Application.Interfaces;

public interface IUserContext
{
    public Guid CurrentUserId { get; }
}
