namespace Evently.Modules.Events.Infrastructure.Persistence;

internal sealed class EventsUnitOfWork : IEventsUnitOfWork, IDisposable, IAsyncDisposable
{
    public EventsUnitOfWork(IDbContextFactory<EventsDbContext> dbContextFactory)
    {
        _context = dbContextFactory.CreateDbContext();

        Categories = new CategoryRepository(_context);
        Events = new EventRepository(_context);
        TicketTypes = new TicketTypeRepository(_context);
    }

    private readonly EventsDbContext _context;
    private IDbContextTransaction? _transaction;

    public ICategoryRepository Categories { get; }
    public IEventRepository Events { get; }
    public ITicketTypeRepository TicketTypes { get; }

    public async Task CreateTransactionAsync(CancellationToken cancellationToken)
    {
        if (_context.Database.CurrentTransaction is not null)
        {
            return;
        }

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (_transaction is null)
        {
            return;
        }

        await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if (_transaction is null)
        {
            return;
        }

        await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
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
