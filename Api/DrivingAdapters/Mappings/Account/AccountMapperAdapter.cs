

using AutoMapper;
using Domain;
using Domain.Models.Account;
using Domain.Ports.Driving.DTOs.Account;

namespace Api.DrivingAdapters.Mappings.Account;

public class AccountMapperAdapter : IAccountMapperPort<IUser, IUserDto>
{
  private readonly IMapper _mapper;

  public AccountMapperAdapter(IMapper mapper)
  {
    _mapper = mapper;
  }
  public IUser MapFrom(IUserDto destination)
  {
    throw new System.NotImplementedException();
  }

  public IUserDto MapTo(IUser source, string token)
  {
    return _mapper.Map<IUserDto>(source, opt => opt.AfterMap((src, dest) => dest.Token = token));
  }
}