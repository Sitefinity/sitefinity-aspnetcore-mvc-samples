using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using component_filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();

builder.Services.AddSingleton<IViewComponentFilter, CustomViewComponentFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSitefinity();

app.Run();
