using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Hexagonal.Api.Configurations;
using Hexagonal.Common.Configurations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.AddEnvironmentVariables()
.Build();

builder.Services.Configure<AppSettings>(config);
builder.Services.AddOptions();

builder.Services.AddControllers();
builder.Services.AddSignalR(e =>
{
    e.EnableDetailedErrors = true;
});

// Health Checks
builder.Services.AddHealthChecks();

// Response Compression
builder.Services.AddResponseCompression();

// Api Versioning
builder.Services.AddApiVersioningConfig();

// Controller config, Serialization and Api Behavior Options
builder.Services.AddControllersConfig();

// Dependency Injection Abstraction
builder.Services.AddDependencyInjectionConfiguration(config);

// Adding Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Adding Authentication
var key = Encoding.ASCII.GetBytes(AuthorizationSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseResponseCompression();
app.UseApiVersioning();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
