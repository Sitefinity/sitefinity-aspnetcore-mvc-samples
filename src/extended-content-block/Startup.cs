using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using extended_content_block.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;

namespace extended_content_block
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSitefinity();
            services.AddViewComponentModels();

            services.AddSingleton<IContentBlockModel, ExtendedContentBlockModel>();
            services.AddSingleton<IEntityExtender, EntityExtender<ContentBlockEntity, ExtendedContentBlockEntity>>();
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
