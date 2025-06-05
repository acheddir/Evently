namespace Evently.Modules.Users.Infrastructure.Users;

public class UserRepository(UsersDbContext context) : IUserRepository
{
    public ValueTask<User?> GetAsync(object id, CancellationToken cancellationToken)
    {
        return context.Users.FindAsync([id], cancellationToken);
    }

    public void Insert(User entity)
    {
        foreach (Role role in entity.Roles)
        {
            context.Attach(role);
        }

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
