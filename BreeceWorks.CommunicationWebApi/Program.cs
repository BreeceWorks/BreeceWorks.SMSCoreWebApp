using BreeceWorks.CommunicationWebApi.Adapters.Implementations;
using BreeceWorks.CommunicationWebApi.Adapters.Interfaces;
using BreeceWorks.CommunicationWebApi.Services.Implementations;
using BreeceWorks.CommunicationWebApi.Services.Interfaces;
using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using AutoMapper;
using BreeceWorks.CommunicationWebApi;
using System.Reflection;
using BreeceWorks.CommunicationWebApi.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddMvcOptions(o=>o.AllowEmptyInputInBodyModelBinding = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BreeceWorks.CommunicationWebApi", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key to identify Authorized User.",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
    c.AddSecurityRequirement(requirement);

    var filePath = Path.Combine(AppContext.BaseDirectory, "BreeceWorks.CommunicationWebApi.xml");
    c.IncludeXmlComments(filePath);

});



builder.Services.AddScoped<IConfigureService, ConfigureService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IMessagingService, MessagingService>();
builder.Services.AddScoped<ITranslatorService, TranslatorService>();
builder.Services.AddScoped<ISMSAdapter, SMSAdapter>();


builder.Services.AddDbContext<ConfigurationDbContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration
       .GetConnectionString("SMSCommunicationDBConnectionString"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
});


builder.Services.AddDbContext<CommunicationDbContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration
       .GetConnectionString("SMSCommunicationDBConnectionString"));
});

builder.Services.AddHttpClient<SMSAdapter>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();

app.Run();