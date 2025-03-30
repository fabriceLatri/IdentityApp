using System;
using Application.Authentication.DTOs.Login;
using AutoMapper;
using Domain.Entities.Users;
using Presentation.Configuration.Profiles.Converters;

namespace Presentation.Configuration.Profiles;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<(IUser, string), ILoginDto>()
            .ConvertUsing<LoginConverter>();
    }
}
