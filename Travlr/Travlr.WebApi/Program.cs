using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.Text;
using Travlr.WebApi.Models;
using Travlr.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// register configuration from appsettings.json 
// that binds to TravlrDatabaseSettings class
builder.Services.Configure<TravlrDatabaseSettings>(
    builder.Configuration.GetSection("TravlrDatabase"));

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


// register implementation of ITripsService as a Singleton service
builder.Services.AddSingleton<ITripsService, TripsService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// 4. Enable Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
