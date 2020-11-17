# MMemoryCacheExtensions

## MemoryCacheExtension
Extension methods for in memory implementation.
Example:
    `cache: {key : value}`
        `{"context_1_1" : "item_1"}`
        `{"context_1_2" : "item_2"}`
        `{"context_2_1" : "item_3"}`
        `{"context_2_1" : "item_4"}`

In order to get list of cached object for context
    `cache.Get<string>(key => key.ToString().StartsWith("context_1"));`

Use `Add` instead of `Set` and `Delete` insted of `Remove` 

## MemoryCacheDecorator

TBD
