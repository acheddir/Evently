namespace Evently.Modules.Users.Infrastructure.Users;

public class UserRepository(UsersDbContext context) : IUserRepository
{
    public Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
