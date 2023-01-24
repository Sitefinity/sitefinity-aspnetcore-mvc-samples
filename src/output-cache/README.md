# Output cache

A custom output cache provider using EF Core local DB to store cache items in a centralized place. This approach is used when having more than one Renderer nodes that need to share the same context. 

## Setup

Run the DB update

``` bash
 dotnet ef database update

```