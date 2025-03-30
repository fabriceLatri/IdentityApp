using Application.Users;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities.Users;
using Domain.Factories.Users;
using Infrastructure.Authentication.Models;

namespace Presentation.Configuration.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<IUser, IUserDto>()
    .ConstructUsing(user => UserDtoFactory.Create(user));

    CreateMap<UserModel, IUser>()
    .ConstructUsing(userModel => UsersFactory.Create(userModel.Id, userModel.Email, userModel.FirstName, userModel.LastName, userModel.IsEmailConfirmed()));
  }
}
