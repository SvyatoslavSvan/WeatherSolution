using AuthorizationModule.Context;
using AuthorizationModule.Factories;
using AuthorizationModule.Models;
using AuthorizationModule.Worker;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenIddict()
    .AddCore(
        opts => opts.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>())
    .AddServer(
        options =>
        {
            options.SetTokenEndpointUris("connect/token");
            options.AllowPasswordFlow();
            options.AddDevelopmentEncryptionCertificate()
                .AddDevelopmentSigningCertificate();
            options.UseAspNetCore().EnableTokenEndpointPassthrough();
            options.DisableAccessTokenEncryption(); //change
            options.RegisterScopes(OpenIddictConstants.Scopes.OpenId, OpenIddictConstants.Scopes.Profile); // might add identity token
        });


builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<DatabaseSeederWorker>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<ApplicationUserClaimsFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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