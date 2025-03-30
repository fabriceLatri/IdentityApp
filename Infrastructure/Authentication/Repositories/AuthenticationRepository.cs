

using Application.Authentication.DTOs.Register;
using Application.Authentication.Respositories;
using Application.Common.Mappers;
using Domain.Entities.Users;
using Domain.Exceptions.Account;
using Domain.Factories.Users;
using Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Respositories
{
  public class AuthenticationRepository: IAuthenticationRepository
  {
    private readonly UserManager<UserModel> _userManager;

    private readonly SignInManager<UserModel> _signInManager;

    private readonly IMapper _mapper;
    public AuthenticationRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMapper mapper)
          {
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
        return UsersFactory.Create(userModel.Id, userModel.Email, userModel.FirstName, userModel.LastName, userModel.IsEmailConfirmed());
      }

      public async Task<IUser?> GetUserByEmail(string email)
      {
        // @TODO: Use ?? operator and throw an exception
        UserModel? userModel = await _userManager.FindByEmailAsync(email);

        if (userModel == null) return null;

        // @TODO: Use AutoMapper here
        IUser user = _mapper.Map<IUser>(userModel);
        
        return user;
      }

      public async Task<IUser> FindUserByEmail(string email)
      {
        IUser user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException("Invalid email or password");

        return user; 
      }

      public async Task CheckPassword(IUser user, string password)
      {
        var result = await _signInManager.CheckPasswordSignInAsync((UserModel)user, password, false);

        if (!result.Succeeded) {
          throw new InvalidCredentialsException("Invalid email or password");
        }
      }
  }
}