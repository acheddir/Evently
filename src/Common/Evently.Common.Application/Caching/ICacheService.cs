namespace Evently.Common.Application.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken);
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expirationTime, CancellationToken cancellationToken);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key, CancellationToken cancellationToken);
}
