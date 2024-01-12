using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.DrivingAdapters.DTOs.Account;
using AutoMapper;
using Domain.Exceptions.Account;
using Domain.Models.Account;
using Domain.Ports.Driving.DTOs.Account;
using Domain.Ports.Driving.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.DrivingAdapters.RestAdapters
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCase _accountCase;

        public AccountController(
            IAccountCase accountCase
            )
        {
            _accountCase = accountCase;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<IUserDto>> RefreshUserToken()
        {
            string emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            IUserDto userDto = await _accountCase.ExecuteRefreshUserToken(emailClaim);

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<IUserDto>> Login(LoginDto model)
        {
            try
            {
                IUserDto user = await _accountCase.ExecuteLogin(model.Email, model.Password);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                var result = await _accountCase.ExecuteRegister(model.FirstName, model.LastName, model.Email, model.Password);

                if (result is IdentityResult identityResult)
                {
                    if (!identityResult.Succeeded) return BadRequest(identityResult.Errors);

                    var responseObject = new { Code = 201, Message = "Your account has been created, you can login" };

                    return CreatedAtAction(nameof(Register), responseObject);
                }
                else
                {
                    return StatusCode(500, "Unexpected type for result");
                }
            }
            catch (EmailAlreadyUsedException emailAlreadyUsedEx)
            {
                return BadRequest(emailAlreadyUsedEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }
        }
    }
}

