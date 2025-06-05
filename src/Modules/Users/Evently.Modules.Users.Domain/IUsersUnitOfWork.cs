namespace Evently.Modules.Users.Domain;

public interface IUsersUnitOfWork : IUnitOfWork
{
    IUserRepository Users { get; }
}
