using Travlr.WebApi.Models;
using Travlr.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// register configuration from appsettings.json 
// that binds to TravlrDatabaseSettings class
builder.Services.Configure<TravlrDatabaseSettings>(
    builder.Configuration.GetSection("TravlrDatabase"));

// register implementation of ITripsService as a Singleton service
builder.Services.AddSingleton<ITripsService, TripsService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
