using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Factories;
using Application.Authentication.Respositories;
using Domain.Entities.Authentication;
using Domain.Entities.Users;
using Domain.Exceptions.Account;
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
        var jwtKey = _config["JWT:Key"];

        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("La clé JWT est manquante ou vide dans la configuration.");
        }

        _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        // _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? ""));
    }
    public string getCredentials(IUser user)
    {
        var tokenDescriptor = GenerateTokenDescriptor(user);
        return GenerateJWT(tokenDescriptor);
    }

    public ICredentialsDto GenerateCredentials(IUser user)
    {
        var tokenDescriptor = GenerateTokenDescriptor(user);

        long expiresInMilliseconds = new DateTimeOffset(tokenDescriptor.Expires ?? DateTime.UtcNow).ToUnixTimeMilliseconds();

        ICredentials creds = new Credentials
        {
            Token = GenerateJWT(tokenDescriptor),
            RefreshToken = GenerateRefreshToken(),
            ExpiresInMilliseconds = expiresInMilliseconds,
        };

        return AuthenticationDtoFactory.CreateCredentialsDto(creds);
    }

    private static string GenerateJWT(SecurityTokenDescriptor tokenDescriptor)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.CreateToken(tokenDescriptor);



        return tokenHandler.WriteToken(jwt);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }

    private SecurityTokenDescriptor GenerateTokenDescriptor(IUser user)
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
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["JWT:ExpiresInMinutes"] ?? "0")),
            SigningCredentials = credentials,
            Issuer = _config["JWT:Issuer"],
        };

        return tokenDescriptor;
    }

    public void CheckCredentials(IUser user, string refreshToken)
    {
        if (user.RefreshToken != refreshToken || user.ExpiresIn < new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()) {
            throw new InvalidCredentialsException("Invalid Refresh Token");
        };
    }
}
