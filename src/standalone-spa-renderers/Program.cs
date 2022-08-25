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
if (app.Environment.IsDevelopment())
{
    app.UseWhen((context) =>
    {
        if (!context.Request.Path.Value.Contains("renderers/react") && !context.Request.Path.Value.Contains("renderers/angular"))
        {
            return true;
        }

        return false;
    }, (appInner) =>
    {
        appInner.UseSitefinity();
    });
}
else
{
    app.UseSitefinity();
}

app.MapSpaRenderer(app.Environment, "React");
app.MapSpaRenderer(app.Environment, "Angular");
app.MapDefaultRenderer(app.Environment);

app.Run();

