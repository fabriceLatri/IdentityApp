using Domain.Entities.Users;

namespace Domain.Factories.Users;

public static class UsersFactory
{
  public static IUser Create(string Id, string? Email, string FirstName, string LastName, bool IsEmailConfirmed)
  {
    return new User()
    {
      Id = Id,
      Email = Email ?? "",
      FirstName = FirstName,
      LastName = LastName,
      IsEmailConfirmed = IsEmailConfirmed
    };
  }
}
