
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Login;
using Application.Authentication.DTOs.RefreshTokens;
using Application.Authentication.DTOs.Register;
using Application.Authentication.UseCases;
using Application.Users.DTOs;
using Domain.Exceptions.Account;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers.Authentication;

namespace Tests.Presentation.Controllers.Authentication;

public class AuthenticationControllerTests
{
    private readonly AuthenticationController _controller;
    private readonly Mock<IRegisterUseCase> _registerUseCaseMock;
    private readonly Mock<ILoginUseCase> _loginUseCaseMock;
    private readonly Mock<IRefreshTokenUseCase> _refreshTokenUseCaseMock;

    public AuthenticationControllerTests()
    {
        _registerUseCaseMock = new Mock<IRegisterUseCase>();
        _loginUseCaseMock = new Mock<ILoginUseCase>();
        _refreshTokenUseCaseMock = new Mock<IRefreshTokenUseCase>();

        _controller = new AuthenticationController(
            _registerUseCaseMock.Object,
            _loginUseCaseMock.Object,
            _refreshTokenUseCaseMock.Object
        );
    }

    [Fact]
    public async Task RefreshToken_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var refreshTokenViewModel = new RefreshTokenViewModel { RefreshToken = "valid_token" };
        var credentialsDtoMock = new Mock<ICredentialsDto>();

        _refreshTokenUseCaseMock.Setup(u => u.Execute("valid_token"))
            .ReturnsAsync(credentialsDtoMock.Object);

        // Act
        var result = await _controller.RefreshToken(refreshTokenViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(credentialsDtoMock.Object, okResult.Value);
    }

    [Fact]
    public async Task RefreshToken_ReturnsUnauthorized_WhenExceptionThrown()
    {
        // Arrange
        var refreshTokenViewModel = new RefreshTokenViewModel { RefreshToken = "invalid_token" };

        _refreshTokenUseCaseMock.Setup(u => u.Execute(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Invalid token"));

        // Act
        var result = await _controller.RefreshToken(refreshTokenViewModel);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Contains("Invalid token", unauthorizedResult.Value.ToString());
    }

    [Fact]
    public async Task Register_ReturnsCreated_WhenSuccessful()
    {
        // Arrange
        var registerDto = new RegisterDTO("John", "Doe", "test@Example.com", "Password123!");
        var userDtoMock = new Mock<IUserDto>();

        _registerUseCaseMock.Setup(u => u.Execute(registerDto))
            .ReturnsAsync(userDtoMock.Object);

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(userDtoMock.Object, createdResult.Value);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenUserCreateExceptionThrown()
    {
        // Arrange
        var registerDto = new RegisterDTO("John", "Doe", "test@Example.com", "Password123!");
        var errors = new List<object> { "Email already exists", "Password too weak" };

        _registerUseCaseMock.Setup(u => u.Execute(It.IsAny<RegisterDTO>()))
            .ThrowsAsync(new UserCreateException(errors));

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var message = badRequestResult.Value?.ToString() ?? string.Empty;
        Assert.Contains("User creation failed", message);
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "Password123!" };
        var loginDtoMock = new Mock<ILoginDto>();

        _loginUseCaseMock.Setup(u => u.Execute(loginViewModel))
            .ReturnsAsync(loginDtoMock.Object);

        // Act
        var result = await _controller.Login(loginViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(loginDtoMock.Object, okResult.Value);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenExceptionThrown()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { Email = "test@example.com", Password = "wrong_password" };

        _loginUseCaseMock.Setup(u => u.Execute(It.IsAny<LoginViewModel>()))
            .ThrowsAsync(new Exception("Invalid email or password"));

        // Act
        var result = await _controller.Login(loginViewModel);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Contains("Invalid email or password", unauthorizedResult.Value.ToString());
    }
}