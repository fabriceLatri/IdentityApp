
namespace Application.Authentication.DTOs.Register;

public interface IRegisterDTO
{
  string FirstName { get; set; }
  string LastName { get; set; }
  string Email { get; set; }
  string Password { get; set; }
  
}
