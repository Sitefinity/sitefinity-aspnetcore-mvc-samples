using PARAGWidgets;
using PARAGWidgets.Models.PARAGAskBox;
using PARAGWidgets.Models.PARAGAssistant;
using PARAGWidgets.Models.PARAGAnswer;
using PARAGWidgets.Models.PARAGResults;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.FormWidgets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSitefinity(x => x.CspOptions.CspDelegate = (cspDirectives, httpContext) =>
{
    cspDirectives.ScriptSrc += " ";
    cspDirectives.StyleSrc += " ";
    cspDirectives.ImgSrc += " ";
    cspDirectives.FontSrc += " data:";
});
builder.Services.AddViewComponentModels();
builder.Services.AddFormViewComponentModels();
builder.Services.AddScoped<IPARAGCDN, PARAGCDN>();
builder.Services.AddScoped<IPARAGAssistantModel, PARAGAssistantModel>();
builder.Services.AddSingleton<IPARAGClient, PARAGClient>();
builder.Services.AddScoped<IPARAGAskBoxModel, PARAGAskBoxModel>();
builder.Services.AddScoped<IPARAGAnswerModel, PARAGAnswerModel>();
builder.Services.AddScoped<IPARAGResultsModel, PARAGResultsModel>();

builder.Services.AddSingleton<PARAGAssistantConfig>((x) =>
{
    var configuration = x.GetRequiredService<IConfiguration>();
    var config = new PARAGAssistantConfig();
    configuration.Bind(PARAGAssistantConfig.SectionName, config);
    return config;
});

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