using System;
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Login;
using Application.Authentication.Respositories;
using Application.Common.Mappers;
using Application.Users.DTOs;
using Domain.Entities.Users;

namespace Application.Authentication.UseCases;

public class LoginUseCase(IAuthenticationRepository authenticationRepository, IAuthenticationSecurity authenticationSecurity, IMapper mapper) : ILoginUseCase
{
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
    private readonly IAuthenticationSecurity _authenticationSecurity = authenticationSecurity;
    private readonly IMapper _mapper = mapper;

    public async Task<ILoginDto>Execute(ILoginViewModel loginViewModel)
    {
        IUser user = await FindUserByEmail(loginViewModel.Email);
        await CheckPassword(user, loginViewModel.Password);

        var creds = _authenticationSecurity.GenerateCredentials(user);


        ILoginDto loginDto = await UpdateUserCredentials(user, creds);

        return loginDto;
    }

    private async Task<IUser> FindUserByEmail(string email)
    {
        IUser user = await _authenticationRepository.FindUserByEmailAsync(email);

        return user;
    }

    private async Task CheckPassword(IUser user, string password)
    {
        await _authenticationRepository.CheckPasswordAsync(user, password);
    }

    private async Task<ILoginDto> UpdateUserCredentials(IUser user, ICredentialsDto credentialsDto)
    {
        await _authenticationRepository.UpdateUserCredentialsAsync(user, credentialsDto);

        return _mapper.Map<ILoginDto>((_mapper.Map<IUserDto>(user), credentialsDto));
    }
}