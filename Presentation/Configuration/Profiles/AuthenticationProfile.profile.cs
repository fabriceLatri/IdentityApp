using System;
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Factories;
using Application.Authentication.DTOs.Login;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities.Authentication;

namespace Presentation.Configuration.Profiles;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<ICredentials, ICredentialsDto>().ConstructUsing(creds => AuthenticationDtoFactory.CreateCredentialsDto(creds));

        CreateMap<(IUserDto userDto, ICredentialsDto CredentialsDto), ILoginDto>()
            .ConvertUsing(src => AuthenticationDtoFactory.CreateLoginDto(src.userDto, src.CredentialsDto));
    }
}
