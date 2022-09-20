using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.Renderer.Designers;

using share_data_between_widgets;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;

var builder = WebApplication.CreateBuilder(args);

// before sitefinity services are added, as there is a preparation for the ContnetList widget as well
builder.Services.AddSingleton<IRequestPreparation, CategoryPreparation>();
// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();

// after the sitefinity services are added so we can override the already registered model.
builder.Services.AddTransient<IContentListModel, OverridenContentListModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSitefinity();

app.UseEndpoints(endpoints =>
{
    endpoints.MapSitefinityEndpoints();
});

app.Run();
