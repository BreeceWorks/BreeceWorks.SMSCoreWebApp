using BreeceWorks.Shared.DbContexts;
using BreeceWorks.Shared.Services;
using BreeceWorks.SMSCoreWebApi.Controllers;
using BreeceWorks.SMSCoreWebApi.Services.Implementation;
using BreeceWorks.SMSCoreWebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new Info
    //{
    //    Version = "v1",
    //    Title = "YourApiName",
    //    Description = "Your Api Description.",
    //    TermsOfService = "None",
    //    Contact = new Contact
    //    { Name = "Contact Title", Email = "contactemailaddress@domain.com", Url = "" }
    //});
    var filePath = Path.Combine(AppContext.BaseDirectory, "BreeceWorks.SMSCoreWebApi.xml");
    c.IncludeXmlComments(filePath);
});


//services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Info
//    {
//        Version = "v1",
//        Title = "YourApiName",
//        Description = "Your Api Description.",
//        TermsOfService = "None",
//        Contact = new Contact
//        { Name = "Contact Title", Email = "contactemailaddress@domain.com", Url = "" }
//    });
//    var filePath = Path.Combine(AppContext.BaseDirectory, "YourApiName.xml");
//    c.IncludeXmlComments(filePath);
//});


builder.Services.AddScoped<IConfigureService, ConfigureService>();
builder.Services.AddScoped<IMediaService, MediaService>();


builder.Services.AddDbContext<ConfigurationDbContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration
       .GetConnectionString("SMSCommunicationDBConnectionString"));
});

builder.Services.AddDbContext<CommunicationDbContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration
       .GetConnectionString("SMSCommunicationDBConnectionString"));
});

builder.Services.AddHttpClient<DemoSMSController>();
builder.Services.AddHttpClient<TwilioSMSController>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
