namespace Application.Users.DTOs;

public record UserDto : IUserDto
{
  public required string Id { get; init; }
  public required string Email { get; init; }
  public required string Firstname { get; init; }
  public required string Lastname { get; init; }
  public bool IsEmailConfirmed { get; init; }
}
