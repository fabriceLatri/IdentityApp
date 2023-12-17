using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Api.DrivenAdapters.DatabaseAdapters;
using Microsoft.AspNetCore.Identity;
using Api.DrivenAdapters.Entities.Account;

namespace Api.DrivenAdapters.AuthentificationAdapters.Configuration
{
	public static class AuthentificationConfigApdater
	{
        public static IServiceCollection AddIdentification(this IServiceCollection services)
        {
            // Defining our Identity Service
            services.AddIdentityCore<User>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoles<IdentityRole>() // be able to add roles
                .AddRoleManager<RoleManager<IdentityRole>>() // to be able to make use of RoleManager
                .AddEntityFrameworkStores<Context>() // providing our context
                .AddSignInManager<SignInManager<User>>() // make use of SignIn Manager
                .AddUserManager<UserManager<User>>() // make use of UserManager to create users
                .AddDefaultTokenProviders(); // to be able to create tokens for email confirmation

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
}

