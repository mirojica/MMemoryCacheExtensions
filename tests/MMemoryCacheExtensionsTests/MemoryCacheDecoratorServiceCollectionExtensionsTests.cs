using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using MMemoryCacheExtensions;
using NUnit.Framework;

namespace MMemoryCacheExtensionsTests
{
    public class MemoryCacheDecoratorServiceCollectionExtensionsTests
    {
        [Test]
        public void Instantiate_memory_cache_decorator()
        {
            var services = new ServiceCollection().AddDecoratedMemoryCache();
            var provider = services.BuildServiceProvider();

            var decoratedMemoryCache = provider.GetRequiredService<IMemoryCache>();

            decoratedMemoryCache.Should().BeOfType<MemoryCacheDecorator>();
        }
    }
}
