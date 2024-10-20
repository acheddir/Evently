namespace Evently.Modules.Ticketing.Infrastructure.Persistence;

internal sealed class TicketingUnitOfWork : ITicketingUnitOfWork, IDisposable, IAsyncDisposable
{
    public TicketingUnitOfWork(IDbContextFactory<TicketingDbContext> dbContextFactory)
    {
        _context = dbContextFactory.CreateDbContext();

        Customers = new CustomerRepository(_context);
    }

    private readonly TicketingDbContext _context;
    private IDbContextTransaction? _transaction;

    public ICustomerRepository Customers { get; }

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
