using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Models;

public class UserModel :  IdentityUser, IUser
{
  public string FirstName { get; set; } = "";

  public string LastName { get; set; } = "";

  public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;

  public string? RefreshToken { get; set; }

  public long? ExpiresIn { get; set; }

  bool IUser.IsEmailConfirmed => IsEmailConfirmed();

  public bool IsEmailConfirmed()
  {
    return EmailConfirmed;
  }
}
