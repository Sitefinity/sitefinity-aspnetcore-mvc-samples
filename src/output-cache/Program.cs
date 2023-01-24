using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();

var connectionString = "Server=(localdb)\\mssqllocaldb;Database=Cache;Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContextFactory<CacheContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<IOutputCacheStore, EfCacheStore>();

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
