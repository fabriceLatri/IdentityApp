using System;
using Application.Authentication.DTOs.Login;
using Application.Authentication.Respositories;
using Application.Common.Mappers;
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

        var token = authenticationSecurity.getCredentials(user);

        return _mapper.Map<ILoginDto>((user, token));
    }

    private async Task<IUser> FindUserByEmail(string email)
    {
        IUser user = await _authenticationRepository.FindUserByEmail(email);

        return user;
    }

    private async Task CheckPassword(IUser user, string password)
    {
        await _authenticationRepository.CheckPassword(user, password);
    }
}