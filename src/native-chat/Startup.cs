using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using native_chat.Attributes;
using native_chat.Client;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.Renderer.Designers;

namespace native_chat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSitefinity();
            services.AddViewComponentModels();
            services.AddSingleton<INativeChatClient, NativeChatClient>();
            services.AddSingleton<IPropertyConfigurator, ExternalPropertyConfigurator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSitefinity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSitefinityEndpoints();
            });
        }
    }
}
