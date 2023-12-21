using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Api;
using Domain.Models.Account;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests;

public class AuthentificationAdapterTest
{
  [Fact]
  public void CreateToken_ShouldGenerateValidJWT()
  {
    // Arrange
    int keySizeInBits = 512;
    var ramdomKey = GenerateRandomKey(keySizeInBits);
    var configMock = new Mock<IConfiguration>();
    configMock.Setup(x => x["JWT:Key"]).Returns(ramdomKey);
    configMock.Setup(x => x["JWT:ExpiresInDays"]).Returns("15");
    configMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5186");

    var userMock = new Mock<IUser>();
    userMock.Setup(x => x.Id).Returns("UserId");
    userMock.Setup(x => x.Email).Returns("user@example.com");
    userMock.Setup(x => x.FirstName).Returns("John");
    userMock.Setup(x => x.LastName).Returns("Doe");

    var jwtAdapter = new JWTAuthentificationAdapter(configMock.Object);

    // Act
    var token = jwtAdapter.CreateToken(userMock.Object);

    // Assert
    token.Should().NotBeNullOrEmpty();

    // You might want to add more specific assertions about the generated token
    // For example, you can decode the token and check its claims
    var handler = new JwtSecurityTokenHandler();
    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
    jsonToken.Should().NotBeNull();
    jsonToken?.Issuer.Should().Be("http://localhost:5186");
    // Add more assertions as needed...
  }

  private string GenerateRandomKey(int keySizeInBits)
  {
    using (var rng = new RNGCryptoServiceProvider())
    {
      byte[] keyBytes = new byte[keySizeInBits / 8];

      rng.GetBytes(keyBytes);

      // Convert the byte array to a hexadecimal string
      string keyString = BitConverter.ToString(keyBytes).Replace("-", string.Empty);

      return keyString;
    }
  }
}
