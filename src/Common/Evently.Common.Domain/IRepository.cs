namespace Evently.Common.Domain;

public interface IRepository<T> where T : class
{
    ValueTask<T?> GetAsync(object id, CancellationToken cancellationToken);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}
