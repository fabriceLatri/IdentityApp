using System;
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.Respositories;
using Domain.Entities.Users;

namespace Application.Authentication.UseCases;

public class RefreshTokenUseCase(IAuthenticationSecurity authenticationSecurity, IAuthenticationRepository authenticationRepository) : IRefreshTokenUseCase
{
    private readonly IAuthenticationSecurity _authenticationSecurity = authenticationSecurity;
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

  public async Task<ICredentialsDto> Execute(string refreshToken)
    {
        IUser user = await _authenticationRepository.FindUserByRefreshToken(refreshToken);

        _authenticationSecurity.CheckCredentials(user, refreshToken);

        ICredentialsDto credentialsDto = _authenticationSecurity.GenerateCredentials(user);

        await _authenticationRepository.UpdateUserCredentialsAsync(user, credentialsDto);

        return credentialsDto;
    }
}
