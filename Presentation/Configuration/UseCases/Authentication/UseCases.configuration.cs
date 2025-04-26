using System;
using Application.Authentication.Respositories;
using Application.Authentication.UseCases;
using Application.Common.Mappers;
using Application.UseCases.Authentication;
using Infrastructure.Authentication.Repositories;
using Presentation.Common.Mappers;

namespace Presentation.Configuration.UseCases;

public static class RegisterUseCaseConfiguration
{
  public static IServiceCollection AddUseCases(this IServiceCollection services)
  {
    services.AddScoped<IRegisterUseCase, RegisterUseCase>();
    services.AddScoped<ILoginUseCase, LoginUseCase>();
    services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
    services.AddScoped<IAuthenticationSecurity, AuthenticationSecurity>();
    services.AddScoped<IMapper, Mapper>();

    return services;
  }
}
