namespace Evently.Modules.Users.Infrastructure.Persistence;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
