using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace MMemoryCacheExtensions
{
    public static class MemoryCacheExtensions
    {
        public const string KEYS = "keys";

        public static T Add<T>(this IMemoryCache cache, object key, T value)
        {
            var item = cache.Set(key, value);
            UpdateKeys(cache, UpdateKeysActions.ADD, key);
            return item;
        }

        public static void Delete(this IMemoryCache cache, object key)
        {
            cache.Remove(key);
            UpdateKeys(cache, UpdateKeysActions.DELETE, key);
        }

        public static IEnumerable<T> Get<T>(this IMemoryCache cache, Predicate<object> predicate)
        {
            if (!cache.TryGetValue(KEYS, out List<object> keys))
                return new List<T>();

            return keys.Where(key => predicate(key)).Select(key => cache.Get<T>(key));
        }

        private static void UpdateKeys(IMemoryCache cache, string action, object key)
        {
            if (!cache.TryGetValue(KEYS, out List<object> keys))
                keys = new List<object>();

            cache.Remove(KEYS);

            if (action == UpdateKeysActions.ADD)
                keys.Add(key);
            else if (action == UpdateKeysActions.DELETE)
                keys.Remove(key);
            cache.Set(KEYS, keys);
        }
    }
}
