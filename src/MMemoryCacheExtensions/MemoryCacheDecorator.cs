using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace MMemoryCacheExtensions
{
    public class MemoryCacheDecorator : IMemoryCache
    {
        private MemoryCache _memoryCache;

        public MemoryCacheDecorator(MemoryCache memoryCache) => _memoryCache = memoryCache;

        public ICacheEntry CreateEntry(object key)
        {
            var cacheEntry = _memoryCache.CreateEntry(key);
            UpdateKeys(_memoryCache, UpdateKeysActions.ADD, key);
            return cacheEntry;
        }

        public void Dispose() => _memoryCache.Dispose();

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
            UpdateKeys(_memoryCache, UpdateKeysActions.REMOVE, key);
        }

        public bool TryGetValue(object key, out object value) =>
            _memoryCache.TryGetValue(key, out value);

        private static void UpdateKeys(IMemoryCache cache, string action, object key)
        {
            if (!cache.TryGetValue(MemoryCacheExtensions.KEYS, out List<object> keys))
                keys = new List<object>();

            cache.Remove(MemoryCacheExtensions.KEYS);

            if (action == UpdateKeysActions.ADD)
                keys.Add(key);
            else if (action == UpdateKeysActions.REMOVE)
                keys.Remove(key);
            cache.Set(MemoryCacheExtensions.KEYS, keys);
        }
    }
}
