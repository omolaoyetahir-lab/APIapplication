using Microsoft.EntityFrameworkCore;
using StudentAPI;
using StudentAPI.Application.Service;
using StudentAPI.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

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