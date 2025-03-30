using Application.Authentication.Respositories;
using Infrastructure.Authentication.Respositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Configuration.Database;

public static class DatabaseConfiguration
{
  public static IServiceCollection AddDatabase(this IServiceCollection services, string databaseConnection)
  {
    services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(databaseConnection, b => b.MigrationsAssembly("Infrastructure")));

    services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

    return services;
  }
}
