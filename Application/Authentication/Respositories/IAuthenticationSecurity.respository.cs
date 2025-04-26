using Application.Authentication.DTOs.Credentials;
using Domain.Entities.Users;

namespace Application.Authentication.Respositories;

public interface IAuthenticationSecurity
{
    string getCredentials(IUser user);
    ICredentialsDto GenerateCredentials(IUser user);
    void CheckCredentials(IUser user, string refreshToken);
}
