# MMemoryCacheExtensions

Decorator for MemoryCache

Example:
    `cache: {key : value}`
        `{"context_1_1" : "item_1"}`
        `{"context_1_2" : "item_2"}`
        `{"context_2_1" : "item_3"}`
        `{"context_2_1" : "item_4"}`


In order to get list of cached object by criteria of key use extension method
    `cache.Get<string>(key => key.ToString().StartsWith("context_1"));`