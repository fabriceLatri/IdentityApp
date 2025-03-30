namespace Application.Authentication.DTOs.Login;

public record LoginDto : ILoginDto
{
  public required string Id { get; init; } 
  public required string Email { get; init; } 
  public required string Firstname { get; init; } 
  public required string Lastname { get; init; } 
  public required bool IsEmailConfirmed { get; init; } 
  public required string Token { get; init; } 
}
