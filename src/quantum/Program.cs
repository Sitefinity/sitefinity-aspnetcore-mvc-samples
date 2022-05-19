using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentList;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.Renderer.Designers;
using Renderer.Attributes;
using Renderer.Client;
using Renderer.Entities.Extends;
using Renderer.Models;
using Renderer.Models.Document;
using Renderer.Models.Extends;
using Renderer.Models.LanguageSelector;
using Renderer.Models.NativeChat;
using Renderer.Models.Testimonial;
using Renderer.Models.LoginStatus;

var builder = WebApplication.CreateBuilder(args);

// add sitefinity related services
builder.Services.AddScoped<ITestimonialModel, TestimonialModel>();
builder.Services.AddScoped<IDocumentModel, DocumentModel>();
builder.Services.AddScoped<IMegaMenuModel, MegaMenuModel>();
builder.Services.AddScoped<LanguageSelectorModel>();
builder.Services.AddSitefinity();
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<IContentListModel, ExtendedContentListModel>();
builder.Services.AddSingleton<IEntityExtender, EntityExtender<ContentListEntity, ExtendedContentListEntity>>();
builder.Services.AddScoped<IFormModel, ExtendedFormModel>();
builder.Services.AddSingleton<IEntityExtender, EntityExtender<FormEntity, ExtendedFormEntity>>();
builder.Services.AddScoped<INativeChatModel, NativeChatModel>();
builder.Services.AddSingleton<INativeChatClient, NativeChatClient>();
builder.Services.AddSingleton<IPropertyConfigurator, ExternalPropertyConfigurator>();
builder.Services.AddScoped<ILoginStatusModel, LoginStatusModel>();

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
