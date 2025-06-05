namespace Evently.Common.Domain;

public interface IUnitOfWork
{
    //This Method will start the database transaction
    Task CreateTransactionAsync(CancellationToken cancellationToken);

    //This Method will commit the database transaction
    Task CommitAsync(CancellationToken cancellationToken);

    //This Method will roll back the database transaction
    Task RollbackAsync(CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
