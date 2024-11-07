namespace Evently.Common.Domain;

public interface IUnitOfWork
{
    //This Method will start the database transaction
    Task CreateTransactionAsync(CancellationToken cancellationToken = default);

    //This Method will commit the database transaction
    Task CommitAsync(CancellationToken cancellationToken = default);

    //This Method will roll back the database transaction
    Task RollbackAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
