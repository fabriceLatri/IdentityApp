using System;
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Login;
using Application.Users.DTOs;
using Domain.Entities.Authentication;

namespace Application.Authentication.DTOs.Factories;

public static class AuthenticationDtoFactory
{
  public static ICredentialsDto CreateCredentialsDto(ICredentials credentials)
  {
    ICredentialsDto credentialsDto = new CredentialsDto
    {
      Token = credentials.Token,
      RefreshToken = credentials.RefreshToken,
      ExpiresInMilliseconds = credentials.ExpiresInMilliseconds
    };

    return credentialsDto;
  }
  public static ILoginDto CreateLoginDto(IUserDto user, ICredentialsDto credentials)
    {
        ILoginDto loginDto = new LoginDto
        {
            User = user,
            Credentials = credentials,
        };

        return loginDto;
    }
}
