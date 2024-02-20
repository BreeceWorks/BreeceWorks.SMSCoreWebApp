using BreeceWorks.CommunicationHub.Data;
using BreeceWorks.CommunicationHub.Data.Contracts;
using BreeceWorks.CommunicationHub.Data.Implementation;
using BreeceWorks.CommunicationHub.Dispatcher.Contracts;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<CommunicationService>();
builder.Services.AddScoped<IDispatcher, BreeceWorks.CommunicationHub.Dispatcher.Implementation.Dispatcher>();
builder.Services.AddScoped<IConfigureService, ConfigureService>(); 
builder.Services.AddScoped<ITranslatorService, TranslatorService>();



builder.Services.AddDbContext<ConfigurationDbContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration
       .GetConnectionString("SMSCommunicationDBConnectionString"));
});

builder.Services.AddHttpClient<Dispatcher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
