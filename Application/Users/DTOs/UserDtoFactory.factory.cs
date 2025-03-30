using Application.Users.DTOs;
using Domain.Entities.Users;

namespace Application.Users;

public static class UserDtoFactory
{
  public static IUserDto Create(IUser user)
  {
    return new UserDto()
    {
      Id = user.Id,
      Email = user.Email,
      Firstname = user.FirstName,
      Lastname = user.LastName,
      IsEmailConfirmed = user.IsEmailConfirmed,
    };
  }
}
