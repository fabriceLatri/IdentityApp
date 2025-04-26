namespace Application.Authentication.DTOs.Credentials;

public record class CredentialsDto: ICredentialsDto
{
  public required string Token { get; init; }
  public required string RefreshToken { get; init; }
  public required long ExpiresInMilliseconds { get; init; }
}
