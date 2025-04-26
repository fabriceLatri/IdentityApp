


using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Register;
using Domain.Entities.Users;

namespace Application.Authentication.Respositories;

public interface IAuthenticationRepository
{
  Task<IUser?> GetUserByEmailAsync(string email);

  Task<IUser> CreateUserAsync(IRegisterDTO newUser);

  Task<IUser> FindUserByEmailAsync(string email);

  Task<IUser> FindUserByRefreshToken(string refreshToken);

  Task CheckPasswordAsync(IUser user, string password);

  Task UpdateUserCredentialsAsync(IUser user, ICredentialsDto credentials);
}