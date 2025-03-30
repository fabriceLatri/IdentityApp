using Application.Authentication.DTOs.Login;
using Application.Authentication.DTOs.Register;
using Application.Authentication.UseCases;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Authentication
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(IRegisterUseCase registerUseCase, ILoginUseCase loginUseCase, IMapper mapper) : ControllerBase
    {
        private readonly IRegisterUseCase _registerUseCase = registerUseCase;

        private readonly ILoginUseCase _loginUseCase = loginUseCase;

        private readonly IMapper _mapper = mapper;

    [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerModel)
        {
            IUser user = await _registerUseCase.Execute(registerModel);

            IUserDto userDto = _mapper.Map<UserDto>(user);

            return Created(string.Empty, userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            ILoginDto loginResponse = await _loginUseCase.Execute(loginViewModel); 

            return Ok(loginResponse);
        }
    }

}
