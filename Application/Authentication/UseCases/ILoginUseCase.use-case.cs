using System;
using Application.Authentication.DTOs.Login;

namespace Application.Authentication.UseCases;

public interface ILoginUseCase
{
    Task<ILoginDto> Execute(ILoginViewModel loginViewModel);
}
