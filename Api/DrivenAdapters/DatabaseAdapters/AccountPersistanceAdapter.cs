using System;
using System.Threading.Tasks;
using Api.DrivenAdapters.Entities.Account;
using Domain.Models.Account;
using Domain.Ports.Driven.Account;
using Microsoft.AspNetCore.Identity;

namespace Api.DrivenAdapters.DatabaseAdapters
{
	public class AccountPersistanceAdapter : IAccountPersistancePort
	{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountPersistanceAdapter(UserManager<User> userManager, SignInManager<User> signInManager)
		{
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IUser> GetUserByEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<bool> VerifyPassword(IUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync((User)user, password, false);

            return result.Succeeded;
        }
    }
}

