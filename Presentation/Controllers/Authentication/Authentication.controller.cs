using System.Security.Claims;
using Application.Authentication.DTOs.Credentials;
using Application.Authentication.DTOs.Login;
using Application.Authentication.DTOs.RefreshTokens;
using Application.Authentication.DTOs.Register;
using Application.Authentication.UseCases;
using Application.Users.DTOs;
using Domain.Exceptions.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Authentication
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(
        IRegisterUseCase registerUseCase,
        ILoginUseCase loginUseCase,
        IRefreshTokenUseCase refreshTokenUseCase
    ) : ControllerBase
    {
        private readonly IRegisterUseCase _registerUseCase = registerUseCase;
        private readonly ILoginUseCase _loginUseCase = loginUseCase;
        private readonly IRefreshTokenUseCase _refreshTokenUseCase = refreshTokenUseCase;

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ICredentialsDto>> RefreshToken(RefreshTokenViewModel refreshTokenViewModel)
        {
            try
            {                
                ICredentialsDto refreshTokenDto = await _refreshTokenUseCase.Execute(refreshTokenViewModel.RefreshToken);

                return Ok(refreshTokenDto);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerModel)
        {
            try
            {
                IUserDto userDto = await _registerUseCase.Execute(registerModel);

                return Created(string.Empty, userDto);
            }
            catch (UserCreateException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (EmailAlreadyUsedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Error. Reason: {ex.Message}" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                ILoginDto loginDto = await _loginUseCase.Execute(loginViewModel);

                return Ok(loginDto);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }

}
