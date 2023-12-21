using Api.DrivenAdapters.Entities.Account;
using Api.DrivingAdapters.DTOs.Account;
using AutoMapper;
using Domain.Models.Account;
using Domain.Ports.Driving.DTOs.Account;

namespace Api.DrivenAdapters.Mappings.Profiles
{
  public class AccountProfile : Profile
  {
    public AccountProfile()
    {
      RegisterAccountProfile();
    }

    private void RegisterAccountProfile()
    {
      CreateMap<User, UserDto>();
      CreateMap<IUser, IUserDto>()
      .Include<User, UserDto>();
    }
  }
}