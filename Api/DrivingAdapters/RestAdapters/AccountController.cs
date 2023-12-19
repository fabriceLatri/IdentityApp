﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.DrivenAdapters.Entities.Account;
using Api.DrivingAdapters.DTOs.Account;
using AutoMapper;
using Domain.Exceptions.Account;
using Domain.Models.Account;
using Domain.Ports.Driving.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.DrivingAdapters.RestAdapters
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountCase _accountCase;

        public AccountController(
            IMapper mapper,
            IAccountCase accountCase
            )
        {
            _mapper = mapper;
            _accountCase = accountCase;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            string emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            IUser user = await _accountCase.ExecuteRefreshUserToken(emailClaim);
            

            return _mapper.Map<UserDto>(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            try
            {
                var user = await _accountCase.ExecuteLogin(model.Email, model.Password);

                return _mapper.Map<UserDto>(user);
            } catch (Exception ex)
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

                    return StatusCode(201, "Your account has been created, you can login");
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

