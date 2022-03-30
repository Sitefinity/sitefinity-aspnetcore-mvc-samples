using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using SandboxWebApp.Models.Testimonial;

namespace SandboxWebApp
{
    /// <summary>
    /// The Startup file.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Startup method")]
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITestimonialModel, TestimonialModel>();
            // add sitefinity related services
            services.AddSitefinity();
            services.AddViewComponentModels();
            services.AddFormViewComponentModels();
        }

        /// <summary>
        /// Configure project.
        /// </summary>
        /// <param name="app">The applicationb builder.</param>
        /// <param name="env">The environment.</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Startup method")]
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // comment to disable endpoint routing
            app.UseStaticFiles();
            app.UseRouting();

            // add sitefinity last, so that the proxied requests do not
            // interfere with the application logic
            // reserved routes handled by the following middleware are /sfrenderer
            // everything else is proxied to sitefinity
            // add your own middleware intercepting logic before the Sitefinity middleware
            app.UseSitefinity();

            // comment to disable endpoint routing
            app.UseEndpoints((endpoints) =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapSitefinityEndpoints();
            });
        }
    }
}
