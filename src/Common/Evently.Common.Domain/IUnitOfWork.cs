namespace Evently.Common.Domain;

public interface IUnitOfWork
{
    //This Method will start the database transaction
    void CreateTransaction();

    //This Method will commit the database transaction
    void Commit();

    //This Method will rollback the database transaction
    void Rollback();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
