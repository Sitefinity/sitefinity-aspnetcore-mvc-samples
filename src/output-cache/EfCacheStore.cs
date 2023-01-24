using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

public class EfCacheStore : IOutputCacheStore
{
    private readonly IDbContextFactory<CacheContext> contextFactory;

    public EfCacheStore(IDbContextFactory<CacheContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public async ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    {
        using (var cacheContext = await this.contextFactory.CreateDbContextAsync(cancellationToken))
        {
            await cacheContext.CacheItems.Where(x => x.Tags.Contains(tag)).ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async ValueTask<byte[]> GetAsync(string key, CancellationToken cancellationToken)
    {
        using (var cacheContext = await this.contextFactory.CreateDbContextAsync(cancellationToken))
        {
            var cacheEntity = await cacheContext.CacheItems.FirstOrDefaultAsync(x => x.Key == key);
            if (cacheEntity != null)
            {
                return cacheEntity.Data;
            }

            return null;
        }
    }

    public async ValueTask SetAsync(string key, byte[] value, string[] tags, TimeSpan validFor, CancellationToken cancellationToken)
    {
        using (var cacheContext = await this.contextFactory.CreateDbContextAsync(cancellationToken))
        {
            var cacheEntity = new CacheEntity()
            {
                Key = key,
                Data = value,
                Tags = string.Join(",", tags),
            };

            await cacheContext.AddAsync(cacheEntity);
            await cacheContext.SaveChangesAsync();
        }
    }
}
