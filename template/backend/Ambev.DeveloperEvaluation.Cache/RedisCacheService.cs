using System.IO.Compression;
using System.Text;
using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly RedisSettings _redisSettings;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    private static readonly JsonSerializerSettings _serializerOptions = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    private static DistributedCacheEntryOptions? _distributedCacheEntryOptions;

    public RedisCacheService(IDistributedCache distributedCache, IOptions<RedisSettings> redisSettings,
        IConnectionMultiplexer connectionMultiplexer)
    {
        _distributedCache = distributedCache;
        _connectionMultiplexer = connectionMultiplexer;
        _redisSettings = redisSettings.Value;

        _distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(_redisSettings.DefaultCacheDurationMinutes),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_redisSettings.DefaultCacheDurationHours)
        };
    }

    public async Task<T> SetAsync<T>(string key, T value, int? durationInMinutes = null)
    {
        var encodedCurrent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, _serializerOptions));
        await _distributedCache.SetAsync(key, await CompressBytesAsync(encodedCurrent), _distributedCacheEntryOptions);

        return value;
    }

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var encodedCached = await _distributedCache.GetAsync(key);

        if (encodedCached == null) return null;

        var cached = Encoding.UTF8.GetString(await DecompressBytesAsync(encodedCached));

        return !string.IsNullOrEmpty(cached) ? JsonConvert.DeserializeObject<T>(cached, _serializerOptions) : null;
    }

    public async Task RemoveAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

    public async Task RemoveAllPrefixAsync(string prefix)
    {
        var server = _connectionMultiplexer.GetServer(_redisSettings.Host);

        var keys = server.Keys(pattern:  _redisSettings.InstanceName + prefix + "*").ToArray();

        if (keys.Length > 0)
        {
            var database = _connectionMultiplexer.GetDatabase();
            foreach (var key in keys)
                await database.KeyDeleteAsync(key);
        }
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory) where T : class
    {
        var cached = await GetAsync<T>(key);

        if (cached != null)
            return cached;

        var generatedValue = await factory();

        return await SetAsync(key, generatedValue);
    }

    public bool TryGetValue<T>(string cacheName, out T? value) where T : class
    {
        value = GetAsync<T>(cacheName).GetAwaiter().GetResult();
        return value != null;
    }

    private static async Task<byte[]> CompressBytesAsync(byte[] bytes,
        CancellationToken cancel = default)
    {
        try
        {
            using var outputStream = new MemoryStream();

            await using (var compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal))
            {
                await compressionStream.WriteAsync(bytes, 0, bytes.Length, cancel);
            }

            return outputStream.ToArray();
        }
        catch
        {
            return bytes;
        }
    }

    private static async Task<byte[]> DecompressBytesAsync(byte[] bytes,
        CancellationToken cancel = default)
    {
        try
        {
            using var inputStream = new MemoryStream(bytes);

            using var outputStream = new MemoryStream();

            await using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                await compressionStream.CopyToAsync(outputStream, cancel);
            }

            return outputStream.ToArray();
        }
        catch
        {
            return bytes;
        }
    }
}