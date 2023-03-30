using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;

using mega_menu.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<IMegaMenuModel, MegaMenuModel>();

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
