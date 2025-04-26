using System;

namespace Application.Authentication.DTOs.Register;

public class RegisterDTO(string firstName, string lastName, string email, string password) : IRegisterDTO
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
