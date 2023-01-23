using System.Runtime.CompilerServices;
using System.Text;
using Merchant.Application;
using Merchant.Common;
using Merchant.Domain;
using Merchant.Persistence;
using Merchant.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddCommon();
builder.Services.AddDomain();
builder.Services.AddPersistence(options =>
    options.UseNpgsql("server=localhost;Port=5432;Database=MerchantDb;User Id=postgres;Password=postgres;"));
builder.Services.AddShared();
builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(configureOptions: jwtBearerOptions =>
    {
        jwtBearerOptions.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "Issuer",
                ValidAudience = "Audience",
                IssuerSigningKey =
                    new SymmetricSecurityKey(key: Encoding
                        .UTF8
                        .GetBytes(s: "dmiWqigAEvWmCq5TgJLhuHvByNY5IonA"))
            };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
