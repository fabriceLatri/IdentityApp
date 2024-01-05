using System.Linq;
using Api;
using Api.DrivenAdapters.AuthentificationAdapters.Configuration;
using Api.DrivenAdapters.DatabaseAdapters;
using Api.DrivenAdapters.DatabaseAdapters.Configuration;
using Api.DrivingAdapters.Configuration;
using Api.DrivingAdapters.DTOs.Account;
using Api.DrivingAdapters.Mappings.Account;
using Domain;
using Domain.Models.Account;
using Domain.Ports.Driven.Account;
using Domain.Ports.Driving.DTOs.Account;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add context for Identity
var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDatabase(dbConnection);

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// UseCases
builder.Services.AddUseCases();


builder.Services.AddSingleton<IAccountAuthentificationPort, JWTAuthentificationAdapter>();
builder.Services.AddScoped<IAccountMapperPort<IUser, IUserDto>, AccountMapperAdapter>();
builder.Services.AddScoped<IAccountPersistancePort, AccountPersistanceAdapter>();

// Defining our Identity Service
builder.Services.AddIdentification();

// To be able to authenticate users using JWT
string jwtKey = builder.Configuration["JWT:Key"];
string jwtIssuer = builder.Configuration["JWT:Issuer"];

builder.Services.AddAuthentification(jwtKey, jwtIssuer);

// Cors
builder.Services.AddCors();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
        .Where(x => x.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();

        var toReturn = new
        {
            Errors = errors
        };

        return new BadRequestObjectResult(toReturn);
    };
});

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["JWT:ClientUrl"]);
});

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

