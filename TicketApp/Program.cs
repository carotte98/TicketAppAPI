using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Services;
using TicketApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Save DbContext on PostgreSql provider
builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// While in development, enable detailled log of SQL queries
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging().EnableDetailedErrors()
    .LogTo(Console.WriteLine,
        LogLevel.Information)
    );
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Ajout des Services
builder.Services.AddScoped<IAppService, AppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
