namespace Evently.Common.Infrastructure.Caching;

internal sealed class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        byte[]? bytes = await cache.GetAsync(key, cancellationToken);

        return bytes is null ? default : Deserialize<T>(bytes);
    }

    public Task<T?> GetAsync<T>(string key)
    {
        return GetAsync<T>(key, CancellationToken.None);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expirationTime, CancellationToken cancellationToken)
    {
        byte[] bytes = Serialize(value);

        return cache.SetAsync(key, bytes, CacheOptions.Create(expirationTime), cancellationToken);
    }

    public Task SetAsync<T>(string key, T value)
    {
        return SetAsync(key, value, null, CancellationToken.None);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken) =>
        cache.RemoveAsync(key, cancellationToken);

    private static T Deserialize<T>(byte[] bytes)
    {
        return JsonSerializer.Deserialize<T>(bytes);
    }

    private static byte[] Serialize<T>(T value)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using var writer = new Utf8JsonWriter(buffer);
        JsonSerializer.Serialize(writer, value);
        return buffer.WrittenSpan.ToArray();
    }
}
