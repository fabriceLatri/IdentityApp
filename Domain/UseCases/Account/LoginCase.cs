using System;
using Domain.Models.Account;
using Domain.Ports.Driven.Account;
using Domain.Ports.Driving.Account;
using Domain.Exceptions.Account;

namespace Domain.UseCases.Account
{
	public class LoginCase : ILoginCase
	{
        private readonly IAccountPersistancePort _accountPersistancePort;

        public LoginCase(IAccountPersistancePort accountPersistancePort)
		{
            _accountPersistancePort = accountPersistancePort;
        }

        public async Task<IUser> Execute(string email, string password)
        {
            IUser? user = await FindUserByEmail(email) ?? throw new UserNotFoundException("Invalid email or password");

            if (!user.EmailConfirmed()) throw new EmailNotConfirmedException("Please confirm your email");

            var result = await VerifyPassword(user, password);

            if (!result) throw new InvalidCredentialsException("Invalid email or password");

            return user;

        }

        private Task<IUser?> FindUserByEmail(String email)
        {
            return _accountPersistancePort.GetUserByEmail(email);
        }

        private Task<bool> VerifyPassword(IUser user, string password)
        {
            return _accountPersistancePort.VerifyPassword(user, password);
        }
    }
}

