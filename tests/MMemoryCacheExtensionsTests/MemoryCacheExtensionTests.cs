using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using MMemoryCacheExtensions;
using System.Collections.Generic;

namespace MMemoryCacheExtensionsTests
{
    public class MemoryCacheExtensionTests
    {
        public IMemoryCache ACache => new MemoryCache(new MemoryCacheOptions());

        [Test]
        public void Add_item_into_cache_and_update_list_of_keys()
        {
            var actualItem = "new item";
            var cache = ACache;

            cache.Add("item_1_key", actualItem);

            cache.Get<List<object>>(MemoryCacheExtensions.KEYS).Should().ContainSingle(key => key.Equals("item_1_key"));
            cache.Get("item_1_key").Should().BeEquivalentTo(actualItem);
        }

        [Test]
        public void Delete_item_from_cache_and_update_list_of_keys()
        {
            var actualItem = "new item 1";
            var cache = ACache;
            cache.Add("item_2_key", actualItem);

            cache.Get<List<object>>(MemoryCacheExtensions.KEYS).Should().ContainSingle(key => key.Equals("item_2_key"));

            cache.Delete("item_2_key");

            cache.Get<List<object>>(MemoryCacheExtensions.KEYS).Should().BeEmpty();
            cache.Get("item_2_key").Should().BeNull();
        }

        [Test]
        public void Get_items_which_keys_satisfy_criteria()
        {
            var cache = ACache;
            cache.Add("item_3_key", "item_3");
            cache.Add("item_4_key", "item_4");
            cache.Add("something_5_key", "item_5");
            cache.Add("something_6_key", "item_6");

            var items = cache.Get<string>(key => key.ToString().StartsWith("item_"));

            items.Should()
                .HaveCount(2).And
                .ContainSingle(item => item.Equals("item_3")).And
                .ContainSingle(item => item.Equals("item_4"));
        }
    }
}