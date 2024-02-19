using BreeceWorks.DemoSMSSimulator.Server.Controllers;
using BreeceWorks.DemoSMSSimulator.Server.Hubs;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
       new[] { "application/octet-stream" });
});

builder.Services.AddScoped<IConfigureService, ConfigureService>();


builder.Services.AddDbContext<ConfigurationDbContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration
       .GetConnectionString("SMSCommunicationDBConnectionString"));
});

builder.Services.AddHttpClient<SignalRController>();
builder.Services.AddHttpClient<ChatHub>();

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

app.Run();
