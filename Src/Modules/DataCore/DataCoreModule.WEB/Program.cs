using Calabonga.UnitOfWork;
using DataCoreModule.Application.Interfaces;
using DataCoreModule.Application.Interfaces.Data;
using DataCoreModule.Application.Interfaces.EnvironmentalMonitoring.ExternalServices;
using DataCoreModule.Application.Profile;
using DataCoreModule.Application.Services;
using DataCoreModule.Infrastructure.Data;
using DataCoreModule.Infrastructure.Data.Context;
using DataCoreModule.Infrastructure.ExternalServices.Implementations;
using DataCoreModule.Infrastructure.ExternalServices.Profile;
using DataCoreModule.Infrastructure.Presenters.Implementations;
using DataCoreModule.Infrastructure.Presenters.Interfaces;
using DataCoreModule.WEB.ExternalApiSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenIddict.Server.AspNetCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IEnvironmentalStateExternalService, EnvironmentalStateExternalService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("WeatherApiSettings").Get<WeatherApiSettings>()!.BaseUrl);
});
builder.Services.AddUnitOfWork<ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWorkAdapter, UnitOfWorkAdapter<ApplicationDbContext>>();
builder.Services.AddScoped<IEnvironmentalService, EnvironmentalService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddAutoMapper(typeof(ExternalServiceMappingProfile), typeof(ApplicationMappingProfile));
builder.Services.AddHostedService<EnvironmentalMonitoringService>();
builder.Services.AddScoped<IEnvironmentalMonitoringPresenter, EnvironmentalMonitoringPresenter>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme;
}
).AddJwtBearer(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, "Bearer", options =>
{
    var authorizationServerUrl = builder.Configuration.GetSection("AuthServer").GetValue<string>("Url");
    options.SaveToken = true;
    options.Authority = authorizationServerUrl;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0, 0, 30)
    };
    options.Events = new JwtBearerEvents()
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            if (string.IsNullOrEmpty(context.Error))
            {
                context.Error = "invalid_token";
            }

            if (string.IsNullOrEmpty(context.ErrorDescription))
            {
                context.ErrorDescription = "This request requires a valid JWT access token to be provided";
            }

            if (context.AuthenticateFailure != null &&
                context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
            {
                var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                context.Response.Headers.Append("x-token-expired", authenticationException?.Expires.ToString("o"));
                context.ErrorDescription = $"The token expired on {authenticationException?.Expires:o}";
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = context.Error,
                error_description = context.ErrorDescription
            }));
        }
    };

});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllAccessPolicy", policy =>
    {
        policy.RequireClaim("Permission", "AllAccess");
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

