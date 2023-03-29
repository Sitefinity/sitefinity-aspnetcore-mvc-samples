using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OutputCaching;
using Progress.Sitefinity.AspNetCore.Areas.Cache;

internal class CustomCachePolicy : PageCachingPolicy
{
    public override async ValueTask CacheRequestAsync(OutputCacheContext ocContext, CancellationToken cancellation)
    {
        await base.CacheRequestAsync(ocContext, cancellation);

        if (ocContext.HttpContext.Request.Path.HasValue && ocContext.HttpContext.Request.Path.Value.Contains("mypage", StringComparison.OrdinalIgnoreCase))
        {
            ocContext.ResponseExpirationTimeSpan = TimeSpan.FromHours(2);
        }
    }
}