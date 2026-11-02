using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .WriteTo.File("log.txt"));

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();

// add razor pages to handle the /Error path
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseWhen(context => IsMediaRequest(context) || !context.IsProxyRequest(), appBuilder =>
    {
        appBuilder.UseStatusCodePagesWithReExecute("/error-pages", "?statusCode={0}");
    });

    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.UseSitefinity();

app.Run();

public partial class Program
{
    private static bool IsMediaRequest(HttpContext httpContext)
    {
        var path = httpContext.Request.Path;
        return path.HasValue && MediaPaths.Any(x => path.Value.StartsWith(x, StringComparison.Ordinal));
    }

    private static readonly string[] MediaPaths = ["/images/", "/docs/", "/videos/"];
}
