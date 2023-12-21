using Api.DrivenAdapters.Entities.Account;
using Api.DrivingAdapters.DTOs.Account;
using Api.DrivingAdapters.RestAdapters;
using AutoMapper;
using Domain.Ports.Driving.UseCases;
using Moq;

namespace Tests;

public class AccountControllerTest
{
  private readonly Mock<IAccountCase> _mockAccountCase;
  private readonly Mock<IMapper> _mockMapper;
  private readonly Mock<AccountController> _mockAccountController;


  public AccountControllerTest()
  {
    _mockAccountCase = new Mock<IAccountCase>();
    _mockMapper = new Mock<IMapper>();
    _mockAccountController = new Mock<AccountController>();
  }

  [Fact]
  public void Login_should_Login_user()
  {
    // Arrange
    var loginModelDto = new LoginDto() { Email = "jdoe@example.com", Password = "123456" };
    var userDto = new UserDto() { FirstName = "John", LastName = "Doe", Token = "azerty" };
    var user = new User();



  }
}
