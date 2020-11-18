using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MMemoryCacheExtensions
{
    public static class MemoryCacheDecoratorServiceCollectionExtensions
    {
        public static IServiceCollection AddDecoratedMemoryCache(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();
            services.TryAdd(ServiceDescriptor
                .Singleton<IMemoryCache>(new MemoryCacheDecorator(new MemoryCache(new MemoryCacheOptions()))));

            return services;
        }

        public static IServiceCollection AddDecoratedMemoryCache(this IServiceCollection services, Action<MemoryCacheOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddMemoryCache();
            services.Configure(setupAction);

            return services;
        }
    }
}
