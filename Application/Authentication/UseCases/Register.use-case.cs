using Application.Authentication.DTOs.Register;
using Application.Authentication.Respositories;
using Domain.Exceptions.Account;
using Domain.Entities.Users;
using Application.Common.Mappers;
using Application.Users.DTOs;
using Application.Authentication.UseCases;

namespace Application.UseCases.Authentication;

public class RegisterUseCase(IAuthenticationRepository authenticationRepository, IMapper mapper) : IRegisterUseCase
{
  
  private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<IUserDto> Execute(IRegisterDTO registerDto)
  {
    IUser? user = await _authenticationRepository.GetUserByEmailAsync(registerDto.Email);

    if (user != null) throw new EmailAlreadyUsedException($"An existing account is using {registerDto.Email} email address. Please try with another one");

    IUser newUser = await _authenticationRepository.CreateUserAsync(registerDto);

    return _mapper.Map<IUserDto>(newUser);
  }
}