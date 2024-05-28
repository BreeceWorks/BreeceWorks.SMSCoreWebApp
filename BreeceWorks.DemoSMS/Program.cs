using BreeceWorks.DemoSMS.Controllers;
using BreeceWorks.DemoSMS.Hubs;
using BreeceWorks.Shared.CustomAuthorization;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomHubAuthorizatioPolicy", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement());
    });
});

builder.Services.AddServerSideBlazor();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.MapFallbackToPage("/_Host");

app.Run();
