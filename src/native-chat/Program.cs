using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.Renderer.Designers;

using native_chat.Attributes;
using native_chat.Client;
using native_chat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<INativeChatModel, NativeChatModel>();
builder.Services.AddSingleton<INativeChatClient, NativeChatClient>();
builder.Services.AddSingleton<IPropertyConfigurator, ExternalPropertyConfigurator>();

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
