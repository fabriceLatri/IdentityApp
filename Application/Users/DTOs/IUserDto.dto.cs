namespace Application.Users.DTOs;

public interface IUserDto
{
  string Id { get; }
  string Email { get; }
  string Firstname { get; }
  string Lastname { get; }
  bool IsEmailConfirmed { get; }
}
