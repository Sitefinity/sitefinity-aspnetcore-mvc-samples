using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.Renderer.Designers;
using Renderer.Attributes;
using Renderer.Client;
using Renderer.Entities.Extends;
using Renderer.Models;
using Renderer.Models.Document;
using Renderer.Models.Extends;
using Renderer.Models.LanguageSelector;
using Renderer.Models.NativeChat;
using Renderer.Models.Testimonial;

namespace Renderer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add sitefinity related services
            services.AddScoped<ITestimonialModel, TestimonialModel>();
            services.AddScoped<IDocumentModel, DocumentModel>();
            services.AddScoped<IMegaMenuModel, MegaMenuModel>();
            services.AddScoped<LanguageSelectorModel>();
            services.AddSitefinity();
            services.AddViewComponentModels();
            services.AddFormViewComponentModels();
            services.AddScoped<IContentListModel, ExtendedContentListModel>();
            services.AddSingleton<IEntityExtender, EntityExtender<ContentListEntity, ExtendedContentListEntity>>();
            services.AddScoped<IFormModel, ExtendedFormModel>();
            services.AddSingleton<IEntityExtender, EntityExtender<FormEntity, ExtendedFormEntity>>();
            services.AddScoped<INativeChatModel, NativeChatModel>();
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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSitefinity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapSitefinityEndpoints();
            });
        }
    }
}