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
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;
using extended_content_block.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<IContentBlockModel, ExtendedContentBlockModel>();
builder.Services.AddSingleton<IEntityExtender, EntityExtender<ContentBlockEntity, ExtendedContentBlockEntity>>();

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
