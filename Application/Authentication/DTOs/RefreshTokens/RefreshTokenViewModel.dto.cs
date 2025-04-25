namespace Application.Authentication.DTOs.RefreshTokens;

public record RefreshTokenViewModel
{
  public required string RefreshToken { get; init; }
}
