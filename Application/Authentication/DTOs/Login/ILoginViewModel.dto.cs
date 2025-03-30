using System;

namespace Application.Authentication.DTOs.Login;

public interface ILoginViewModel
{
  string Email { get; }
  string Password { get; }
}
