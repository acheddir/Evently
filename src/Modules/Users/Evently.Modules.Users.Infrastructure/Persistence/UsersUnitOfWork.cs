namespace Evently.Modules.Users.Infrastructure.Persistence;

internal sealed class UsersUnitOfWork : IUsersUnitOfWork, IDisposable, IAsyncDisposable
{
    public UsersUnitOfWork(IDbContextFactory<UsersDbContext> dbContextFactory)
    {
        _context = dbContextFactory.CreateDbContext();

        Users = new UserRepository(_context);
    }

    private readonly UsersDbContext _context;
    private IDbContextTransaction? _transaction;

    public IUserRepository Users { get; }

    public void CreateTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        _transaction?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }
    }
}
