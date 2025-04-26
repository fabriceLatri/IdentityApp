using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Mvc;
using Presentation.Configuration.Database;
using Presentation.Configuration.Identification;
using Presentation.Configuration.Profiles;
using Presentation.Configuration.UseCases;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
});

if (builder.Environment.IsDevelopment()) {
    var envPath = Path.Combine("..", ".env");

    builder.Configuration.AddDotNetEnv(envPath, DotNetEnv.LoadOptions.TraversePath());
}

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var dbConnection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDatabase(dbConnection);
builder.Services.AddUseCases();
builder.Services.AddIdentification();

// To be able to authenticate users using JWT
string jwtKey = builder.Configuration["JWT:Key"]!;
string jwtIssuer = builder.Configuration["JWT:Issuer"]!;

builder.Services.AddAuthentification(jwtKey, jwtIssuer);

// AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(AuthenticationProfile));

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

if (!app.Environment.IsDevelopment()) 
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
foreach (var endpoint in endpointDataSource.Endpoints)
{
    Console.WriteLine($"Endpoint: {endpoint.DisplayName}");
}

app.Run();

