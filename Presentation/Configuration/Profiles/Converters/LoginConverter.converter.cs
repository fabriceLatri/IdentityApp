using Application.Authentication.DTOs.Factories;
using Application.Authentication.DTOs.Login;
using AutoMapper;
using Domain.Entities.Users;

namespace Presentation.Configuration.Profiles.Converters;

public class LoginConverter : ITypeConverter<(IUser user, string credential), ILoginDto>
{
  public ILoginDto Convert((IUser user, string credential) source, ILoginDto destination, ResolutionContext context)
  {
    return LoginDtoFactory.Create(source.user, source.credential);
  }
}
