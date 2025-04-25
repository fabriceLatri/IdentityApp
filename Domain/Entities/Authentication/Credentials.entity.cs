namespace Domain.Entities.Authentication;

public record class Credentials: ICredentials
{
  public required string Token { get; init; }
  public required string RefreshToken { get; init; }
  public required long ExpiresInMilliseconds { get; init; }
}
