using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Services;
using TicketApp.Services.Interfaces;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketApp", Version = "v1" });
});

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

// CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Ajout des Services
builder.Services.AddScoped<IAppService, AppService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "TicketAppV1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
