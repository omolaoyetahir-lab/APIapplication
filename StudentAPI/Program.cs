using Microsoft.EntityFrameworkCore;
using Serilog;
using StudentAPI;
using StudentAPI.Application.Service;
using StudentAPI.Infrastructure.Context;
using Microsoft.Extensions.Options;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "StudentAPI")
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
);

builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Repository to the Container
builder.Services.AddScoped<IStudentRepositories, StudentRepositories>();

// Add services to the container
builder.Services.AddScoped<IStudentService, StudentServices>();

// Add SQL Server
builder.Services.AddSqlServer<ApplicationDbContext>(
    builder.Configuration.GetConnectionString("DefaultConnection")
);

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();