using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Authentication.Respositories;
using Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication.Repositories;

public class AuthenticationSecurity : IAuthenticationSecurity
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _jwtKey;
    public AuthenticationSecurity(IConfiguration config)
    {
        _config = config;
        _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? ""));
    }
    public string getCredentials(IUser user)
    {
        return CreateJWT(user);
    }

    private string CreateJWT(IUser user)
    {
        List<Claim> userClaims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
        ];

        var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"] ?? "0")),
            SigningCredentials = credentials,
            Issuer = _config["JWT:Issuer"],
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwt);
    }
}
