using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OutputCaching;
using Progress.Sitefinity.AspNetCore.Web.Caching;

internal class CustomCachePolicy : PageCachingPolicy
{
    public override async ValueTask CacheRequestAsync(OutputCacheContext ocContext, CancellationToken cancellation)
    {
        await base.CacheRequestAsync(ocContext, cancellation);
        ocContext.ResponseExpirationTimeSpan = TimeSpan.FromSeconds(2);
    }
}