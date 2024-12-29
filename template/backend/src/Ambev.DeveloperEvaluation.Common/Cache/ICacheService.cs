namespace Ambev.DeveloperEvaluation.Common.Cache;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key) where T : class;
    Task<T> SetAsync<T>(string key, T value, int? durationInMinutes = null);
    Task RemoveAsync(string key);
    Task RemoveAllPrefixAsync(string key);

    Task<T> GetOrCreateAsync<T>(string cacheName, Func<Task<T>> factory) where T : class?;

    public bool TryGetValue<T>(string cacheName, out T? value) where T : class;
}