using Application.Authentication.DTOs.Credentials;
using Application.Users.DTOs;

namespace Application.Authentication.DTOs.Login;

public interface ILoginDto
{
  public ICredentialsDto Credentials { get; }
  public IUserDto User { get; }
}
