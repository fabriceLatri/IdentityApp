using System;
using Domain.Entities.Users;

namespace Application.Authentication.Respositories;

public interface IAuthenticationSecurity
{
    string getCredentials(IUser user);
}
