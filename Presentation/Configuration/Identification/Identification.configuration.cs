using Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Presentation.Configuration.Identification;

public static class IdentificationConfiguration
{ 
  public static IServiceCollection AddIdentification(this IServiceCollection services)
  {
    services.AddIdentityCore<UserModel>(options => 
    {
      options.Password.RequiredLength = 6;
      options.Password.RequireDigit = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.SignIn.RequireConfirmedEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<AuthenticationContext>()
    .AddSignInManager<SignInManager<UserModel>>()
    .AddUserManager<UserManager<UserModel>>()
    .AddDefaultTokenProviders();

    return services;
  }

  public static IServiceCollection AddAuthentification(this IServiceCollection services, string jwtKey, string jwtIssuer)
		{
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer((options) => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          // Validate the token based on the key we have provided inside appsettings.development.json JWT:Key
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
          ValidIssuer = jwtIssuer,
          ValidateIssuer = true,
          ValidateAudience = false
        };
      });

      return services;
  }
}
