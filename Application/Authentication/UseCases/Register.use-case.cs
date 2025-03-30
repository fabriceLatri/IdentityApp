using Application.Authentication.DTOs.Register;
using Application.Authentication.Respositories;
using Application.Authentication.UseCases;
using Domain.Exceptions.Account;
using Domain.Entities.Users;

namespace Application.UseCases.Authentication;

public class RegisterUseCase(IAuthenticationRepository authenticationRepository) : IRegisterUseCase
{
  
  private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

  public async Task<IUser> Execute(IRegisterDTO registerDto)
  {
    IUser? user = await _authenticationRepository.GetUserByEmail(registerDto.Email);

    if (user != null) throw new EmailAlreadyUsedException($"An existing account is using {registerDto.Email} email address. Please try with another one");

    IUser newUser = await _authenticationRepository.CreateUserAsync(registerDto);

    return newUser;
  }
}