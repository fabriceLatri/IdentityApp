using Application.Authentication.DTOs.Credentials;
using Application.Users.DTOs;

namespace Application.Authentication.DTOs.Login;

public record LoginDto : ILoginDto
{
  public required ICredentialsDto Credentials { get; init; }

  public required IUserDto User { get; init; }
}
