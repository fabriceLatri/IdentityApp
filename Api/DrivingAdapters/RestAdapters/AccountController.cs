using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.DrivenAdapters.Entities.Account;
using Api.DrivingAdapters.DTOs.Account;
using AutoMapper;
using Domain.Models.Account;
using Domain.Ports.Driving.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoginCase _loginCase;

        public AccountController(
            JWTService jwtService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMapper mapper,
            ILoginCase loginCase
            )
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _loginCase = loginCase;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            IUser user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);

            return CreateApplicationUserDto(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            try
            {
                var user = await _loginCase.Execute(model.Email, model.Password);

                return _mapper.Map<UserDto>(user);
            } catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExists(model.Email)) return BadRequest($"An existing account is using {model.Email} email address. Please try with another one");

            User userToAdd = new()
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new ObjectResult(value: "Your account has been created, you can login") { StatusCode = StatusCodes.Status201Created };
        }

        #region Private Helper Methods
        private UserDto CreateApplicationUserDto(IUser user)
        {
            UserDto dto = _mapper.Map<UserDto>(user);

            return dto;
        }

        private async Task<bool> CheckEmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        #endregion
    }
}

