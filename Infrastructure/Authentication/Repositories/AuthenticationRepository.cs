

using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Register;
using Application.Authentication.Respositories;
using Application.Common.Mappers;
using Domain.Entities.Users;
using Domain.Exceptions.Account;
using Infrastructure.Authentication.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Respositories
{
  public class AuthenticationRepository: IAuthenticationRepository
  {
    private readonly AuthenticationContext _dbContext;
    private readonly UserManager<UserModel> _userManager;

    private readonly SignInManager<UserModel> _signInManager;

    private readonly IMapper _mapper;
    public AuthenticationRepository(AuthenticationContext dbContext, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMapper mapper)
          {
              _dbContext = dbContext;
              _userManager = userManager;
              _signInManager = signInManager;
              _mapper = mapper;
          }
      public async Task<IUser> CreateUserAsync(IRegisterDTO newUser)
      {
        UserModel userModel = new()
        {
          FirstName = newUser.FirstName,
          LastName = newUser.LastName,
          UserName = newUser.Email,
          Email = newUser.Email,
          EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(userModel, newUser.Password);

        if (!result.Succeeded) {
          throw new UserCreateException(result.Errors);
        }
        return userModel;
      }

      public async Task<IUser?> GetUserByEmailAsync(string email)
      {
        // @TODO: Use ?? operator and throw an exception
        UserModel? userModel = await _userManager.FindByEmailAsync(email);

        if (userModel == null) return null;

        IUser user = _mapper.Map<IUser>(userModel);
        
        return user;
      }

      public async Task<IUser> FindUserByEmailAsync(string email)
      {
        IUser user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException("Invalid email or password");

        return user; 
      }

      public async Task CheckPasswordAsync(IUser user, string password)
      {
        var result = await _signInManager.CheckPasswordSignInAsync((UserModel)user, password, false);

        if (!result.Succeeded) {
          throw new InvalidCredentialsException("Invalid email or password");
        }
      }

      public async Task UpdateUserCredentialsAsync(IUser user, ICredentialsDto credentials)
      {
        UserModel userModel = await _userManager.FindByEmailAsync(user.Email ?? "") ?? throw new UserNotFoundException("User not found");
        userModel.RefreshToken = credentials.RefreshToken;
        userModel.ExpiresIn = credentials.ExpiresInMilliseconds;

        var result = await _userManager.UpdateAsync(userModel);

        if (!result.Succeeded) {
          throw new UserCreateException(result.Errors);
        }
      }

    public async Task<IUser> FindUserByRefreshToken(string refreshToken)
    {
        IUser user = await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken) ?? throw new UserNotFoundException("Invalid email or password");

        return user;
      }
  }
}