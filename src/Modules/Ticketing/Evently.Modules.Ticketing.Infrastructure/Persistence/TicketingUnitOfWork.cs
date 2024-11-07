namespace Evently.Modules.Ticketing.Infrastructure.Persistence;

internal sealed class TicketingUnitOfWork : ITicketingUnitOfWork, IDisposable, IAsyncDisposable
{
    public TicketingUnitOfWork(IDbContextFactory<TicketingDbContext> dbContextFactory)
    {
        _context = dbContextFactory.CreateDbContext();

        Customers = new CustomerRepository(_context);
        TicketTypes = new TicketTypeRepository(_context);
        Events = new EventRepository(_context);
    }

    private readonly TicketingDbContext _context;
    private IDbContextTransaction? _transaction;

    public ICustomerRepository Customers { get; }
    public ITicketTypeRepository TicketTypes { get; }
    public IEventRepository Events { get; }
    public IOrderRepository Orders { get; }
    public IPaymentRepository Payments { get; }
    public ITicketRepository Tickets { get; }

    public async Task CreateTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_context.Database.CurrentTransaction is not null)
        {
            return;
        }

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            return;
        }

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            return;
        }

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
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
