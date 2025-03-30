using System;
using Application.Authentication.DTOs.Login;
using Domain.Entities.Users;

namespace Application.Authentication.DTOs.Factories;

public static class LoginDtoFactory
{
    public static ILoginDto Create(IUser user, string token)
    {
        ILoginDto loginDto = new LoginDto
        {
            Id = user.Id,
            Email = user.Email ?? "",
            Firstname = user.FirstName,
            Lastname = user.LastName,
            IsEmailConfirmed = user.IsEmailConfirmed,
            Token = token
        };

        return loginDto;
    }
}
