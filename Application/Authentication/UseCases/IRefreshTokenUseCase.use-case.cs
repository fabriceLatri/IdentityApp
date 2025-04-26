

using Application.Authentication.DTOs.Credentials;

namespace Application.Authentication.UseCases;

public interface IRefreshTokenUseCase
{
    public Task<ICredentialsDto> Execute(string refreshToken);
}
