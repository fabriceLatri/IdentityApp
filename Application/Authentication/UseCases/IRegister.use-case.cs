using Application.Authentication.DTOs.Register;
using Domain.Entities.Users;

namespace Application.Authentication.UseCases;

public interface IRegisterUseCase
{
  Task<IUser> Execute(IRegisterDTO registerDTO);
}
