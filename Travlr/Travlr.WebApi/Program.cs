using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using Travlr.WebApi.Models;
using Travlr.WebApi.Repository;
using Travlr.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// load JWT settings from appsettings.json
var mongoDbSettings = builder.Configuration.GetSection("TravlrDatabase");
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// See README on how to configure user secret store
var jwtSecret = builder.Configuration["JwtSettings:Secret"];
var key = Encoding.ASCII.GetBytes(jwtSecret!);

// 2. Configure MongoDB Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
})
.AddMongoDbStores<ApplicationUser, ApplicationRole, ObjectId>(
    mongoDbSettings["ConnectionString"],
    mongoDbSettings["DatabaseName"]
)
.AddDefaultTokenProviders();

// 3. Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true
    };
});

var mongoConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
var mongoDatabaseName = builder.Configuration["TravlrDatabase:DatabaseName"];
var tripsCollection = builder.Configuration["TravlrDatabase:TripsCollectionName"];

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(mongoConnectionString));
builder.Services.AddScoped(s => s.GetRequiredService<IMongoClient>().GetDatabase(mongoDatabaseName));

// Register repositories and services
builder.Services.AddScoped<IRepository<Trip>>(provider =>
    new GenericRepository<Trip>(provider.GetRequiredService<IMongoDatabase>(), "trips"));

// register implementation of ITripsService as a Scoped service
builder.Services.AddScoped<ITripsService, TripsService>();

// Cross Origin Resource Sharing to allow the frontend to
// send requests to our API

var allowTravlrFrontendOrigins = "_travlrFrontendOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowTravlrFrontendOrigins,
                      policy =>
                      {
                          // TODO: refactor URLs to appsettings.json
                          // these are the Node.js and App Admin URLs
                          policy.WithOrigins("http://localhost:3000", "http://localhost:4200") 
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

// 4. Enable Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseCors(allowTravlrFrontendOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
