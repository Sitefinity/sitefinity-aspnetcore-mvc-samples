# Output cache

A custom output cache provider using EF Core local DB to store cache items in a centralized place. This approach is used when having more than one Renderer nodes that need to share the same context. This sample is based on the official Asp.Net Core [output cache implementation](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output) and Sitefinity Renderer >=14.4

## Setup

Run the DB update

``` bash
 dotnet ef database update

```

## Renderer configurations
In the class [CustomCachePolicy](./CustomCachePolicy.cs) the default time of the cache duration is specified. Here you can configure different cache durations per page request. More info on cache policies can be found [here](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output).