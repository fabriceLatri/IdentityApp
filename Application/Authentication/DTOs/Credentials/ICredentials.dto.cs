
namespace Application.Authentication.DTOs.Credentials;

public interface ICredentialsDto
{
  public string Token { get; }
  public string RefreshToken { get; }
  public long ExpiresInMilliseconds { get; }
}
