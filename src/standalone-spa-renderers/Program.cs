using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();

// registers the renderers within the folders Angular and React
builder.Services.AddJavaScriptRenderers(builder.Environment);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.Run();

