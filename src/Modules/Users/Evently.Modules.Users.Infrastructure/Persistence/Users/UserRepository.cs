namespace Evently.Modules.Users.Infrastructure.Persistence.Users;

public class UserRepository(UsersDbContext context) : IUserRepository
{
    public ValueTask<User?> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        return context.Users.FindAsync([id], cancellationToken);
    }

    public void Insert(User entity)
    {
        context.Users.Add(entity);
    }

    public void Update(User entity)
    {
        context.Users.Update(entity);
    }

    public void Delete(User entity)
    {
        context.Users.Remove(entity);
    }
}
