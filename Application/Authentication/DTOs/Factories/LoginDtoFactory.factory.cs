using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Login;
using Application.Users.DTOs;

namespace Application.Authentication.DTOs.Factories;

public static class LoginDtoFactory
{
    public static ILoginDto Create(IUserDto user, ICredentialsDto credentials)
    {
        ILoginDto loginDto = new LoginDto
        {
            User = user,
            Credentials = credentials,
        };

        return loginDto;
    }
}
