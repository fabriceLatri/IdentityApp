using Domain.Models.Account;
using Domain.Ports.Driving.DTOs.Account;

namespace Domain;

public interface IAccountAuthentificationPort
{
  public string CreateToken(IUser user);
}
