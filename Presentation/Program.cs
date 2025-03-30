using Microsoft.AspNetCore.Mvc;
using Presentation.Configuration.Database;
using Presentation.Configuration.Identification;
using Presentation.Configuration.Profiles;
using Presentation.Configuration.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDatabase(dbConnection);
builder.Services.AddRegisterUseCase();
builder.Services.AddIdentification();

// To be able to authenticate users using JWT
string jwtKey = builder.Configuration["JWT:Key"]!;
string jwtIssuer = builder.Configuration["JWT:Issuer"]!;

builder.Services.AddAuthentification(jwtKey, jwtIssuer);

// AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(LoginProfile));

// Cors
builder.Services.AddCors();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
        .Where(x => x.Value?.Errors.Count > 0)
        .SelectMany(x => x.Value!.Errors)
        .Select(x => x.ErrorMessage).ToArray();

        var toReturn = new
        {
            Errors = errors
        };

        return new BadRequestObjectResult(toReturn);
    };
});


var app = builder.Build();

app.UseCors(opt => {
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["JWT:ClientUrl"]!);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

