namespace Application.Authentication.DTOs.Login;

public class LoginViewModel : ILoginViewModel
{
  public required string Email { get; set; }
  public required string Password { get; set; }
}
