using Application.Users.DTOs;

namespace Application.Authentication.DTOs.Login;

public interface ILoginDto : IUserDto
{
  public string Token { get; }
}
