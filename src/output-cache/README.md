# Output cache

This example demonstrates three ways to customize the output cache in the Renderer.

1. Change the default absolute expiration of the cache via settings. The Setting is located under Sitefinity::OutputCacheDuration and is a value (in seconds) that tells the renderer how long to cache each response.

2. Override the default caching policy -> [CustomCachePolicy](./CustomCachePolicy.cs). Useful when you wish to have more fine grain control over what gets cached. For more information on the matter checkout [this article](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output)

3. Distributed cache provider

A custom output cache provider using EF Core local DB to store cache items in a centralized place. This approach is used when having more than one Renderer nodes that need to share the same context. This sample is based on the official Asp.Net Core [output cache implementation](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output) and Sitefinity Renderer >=14.4

## Setup

Run the DB update

``` bash
 dotnet ef database update

```
