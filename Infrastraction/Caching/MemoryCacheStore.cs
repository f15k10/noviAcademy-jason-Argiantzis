
using Microsoft.Extensions.Caching.Memory;
using NoviCode.Application.Cache;

namespace NoviCode.Infrastructure.Caching
{
    public class MemoryCacheStore : ICache
    {
        public readonly IMemoryCache _cache;

        public MemoryCacheStore(IMemoryCache cache) { _cache = cache; }

        public bool TryGet<T>(string key,out T? value) => _cache.TryGetValue(key, out value);

        public void Set<T>(string key,T value,TimeSpan ttl) => _cache.Set(key, value, ttl);

        public void Remove(string key)=> _cache.Remove(key);
    }
}
