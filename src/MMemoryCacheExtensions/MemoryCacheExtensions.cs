using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace MMemoryCacheExtensions
{
    public static class MemoryCacheExtensions
    {
        public const string KEYS = "keys";

        public static IEnumerable<T> Get<T>(this IMemoryCache cache, Predicate<object> predicate)
        {
            if (!cache.TryGetValue(KEYS, out List<object> keys))
                return new List<T>();

            return keys.Where(key => predicate(key)).Select(key => cache.Get<T>(key));
        }
    }
}
