using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.Renderer.Designers;

using all_properties.Extensibility;
using System.Collections.Generic;
using Progress.Sitefinity.Renderer.Designers.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();

builder.Services.AddSingleton<IPropertyConfigurator, ExternalChoicePropertyConfigurator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("getcustomchoices"))
    {
        var refererVal = context.Request.Headers.Referer.ToString();
        var query = new Uri(refererVal).Query;
        var parsed = HttpUtility.ParseQueryString(query);
        var culture = parsed["sf_culture"];
        
        var choices = new List<ChoiceValueDto>()
        {
            new ChoiceValueDto("Option 1 name", "Option 1" + culture),
            new ChoiceValueDto("Option 2 name", "Option 2" + culture)
        };

        var valueToSerialize = JObject.FromObject(new
        {
            value = choices
        });

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(valueToSerialize.ToString());
        return;
    }

    await next();
});

app.UseStaticFiles();
app.UseRouting();
app.UseSitefinity();

app.Run();
