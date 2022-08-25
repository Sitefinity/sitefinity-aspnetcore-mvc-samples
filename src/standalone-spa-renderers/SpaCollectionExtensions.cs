// <summary>
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.Clients.LayoutService.Dto;
/// The extensions for the service collection.
/// </summary>
public static class SpaCollectionExtensions
{
    public static void MapDefaultRenderer(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSitefinity();
        app.MapWhen((context =>
        {
            if (context.Items.TryGetValue("sfpagemodel", out object model) && model is PageModelDto pageModelDto)
            {
                if (!pageModelDto.MetaInfo.Title.Contains("React", System.StringComparison.OrdinalIgnoreCase) && !pageModelDto.MetaInfo.Title.Contains("Angular", System.StringComparison.OrdinalIgnoreCase))
                {
                        return true;
                }
            }

            return false;
        }), (appInner) =>
        {
            appInner.UseRouting();
            appInner.UseEndpoints((endpoints) =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapSitefinityEndpoints();
                endpoints.MapControllers();
            });
        });
    }

    public static void MapSpaRenderer(this IApplicationBuilder app, IWebHostEnvironment env, string rendererName, string devServerUrl = null)
    {
        app.MapWhen((context =>
        {
            if (context.Items.TryGetValue("sfpagemodel", out object model) && model is PageModelDto pageModelDto)
            {
                if (pageModelDto.MetaInfo.Title.Contains(rendererName, System.StringComparison.OrdinalIgnoreCase))
                {
                        return true;
                }
            }

            if (env.IsDevelopment() && !string.IsNullOrEmpty(devServerUrl))
            {
                if (context.Request.Path.Value.Contains($"renderers/{rendererName}", System.StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }), (appInner) =>
        {
            appInner.UseWhen((context) =>
            {
                var renderContext = context.RequestServices.GetRequiredService<IRenderContext>();
                return renderContext.IsLive();
            }, appInnerInner =>
            {
                appInnerInner.UseSpa((config) =>
                {
                    config.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/renderers/{rendererName}"))
                    };

                    if (env.IsDevelopment() && !string.IsNullOrEmpty(devServerUrl))
                    {
                        config.UseProxyToSpaDevelopmentServer(devServerUrl);
                    }
                });
            });
        });
    }
}
