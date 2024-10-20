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
