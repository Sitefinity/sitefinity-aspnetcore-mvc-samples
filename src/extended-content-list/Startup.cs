using extended_content_list.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;

namespace extended_content_list
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSitefinity();
            services.AddViewComponentModels();
            services.AddScoped<IContentListModel, ExtendedContentListModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSitefinity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSitefinityEndpoints();
                var diagnosticUrl = Constants.PrefixRendererUrl("{controller}/{action=Index}");
                endpoints.MapControllerRoute("Content List", diagnosticUrl, null);
            });
        }
    }
}
