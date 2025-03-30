


using Application.Authentication.DTOs.Register;
using Domain.Entities.Users;

namespace Application.Authentication.Respositories;

public interface IAuthenticationRepository
{
  Task<IUser?> GetUserByEmail(string email);

  Task<IUser> CreateUserAsync(IRegisterDTO newUser);

  Task<IUser> FindUserByEmail(string email);

  Task CheckPassword(IUser user, string password);
}