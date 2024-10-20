namespace Evently.Modules.Users.Domain;

public interface IUsersUnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get; }
}
