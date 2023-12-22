using Api.DrivenAdapters.Entities.Account;
using Api.DrivenAdapters.Mappings.Profiles;
using Api.DrivingAdapters.DTOs.Account;
using Api.DrivingAdapters.Mappings.Account;
using AutoMapper;
using Domain;
using Domain.Models.Account;
using Domain.Ports.Driving.DTOs.Account;
using FluentAssertions;
using Moq;

namespace Tests;

public class AccountMapperAdapterTest
{
  private static IMapper _mapper;

  public AccountMapperAdapterTest()
  {
    if (_mapper == null)
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new AccountProfile());
      });
      IMapper mapper = mappingConfig.CreateMapper();
      _mapper = mapper;
    }
  }
  [Fact]
  public void MapTo_should_map_user_to_userDto()
  {
    // Arrange
    string token = "eazdfgpluesd1zerc1";
    IUserDto expectedUserDto = new UserDto()
    {
      FirstName = "John",
      LastName = "Smith",
      Token = "eazdfgpluesd1zerc1"
    };
    IUser user = new User()
    {
      FirstName = "John",
      LastName = "Smith",
    };

    IAccountMapperPort<IUser, IUserDto> mapper = new AccountMapperAdapter(_mapper);

    // Act
    IUserDto userDto = mapper.MapTo(user, token);

    // Assertions
    // Assert

    userDto.Should().BeEquivalentTo(expectedUserDto);
  }
}
