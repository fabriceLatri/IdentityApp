using Application.Authentication.DTOs.Register;
using Application.Users.DTOs;

namespace Application.Authentication.UseCases;

public interface IRegisterUseCase
{
  Task<IUserDto> Execute(IRegisterDTO registerDTO);
}
