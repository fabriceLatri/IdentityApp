using System;
using Domain.Models.Account;
using Domain.Ports.Driven.Account;
using Domain.Ports.Driving.Account;
using Domain.Exceptions.Account;

namespace Domain.UseCases.Account
{
	public class AccountCase : IAccountCase
	{
        private readonly IAccountPersistancePort _accountPersistancePort;

        public AccountCase(IAccountPersistancePort accountPersistancePort)
		{
            _accountPersistancePort = accountPersistancePort;
        }

        public async Task<IUser> ExecuteLogin(string email, string password)
        {
            IUser? user = await FindUserByEmail(email) ?? throw new UserNotFoundException("Invalid email or password");

            if (!user.EmailConfirmed()) throw new EmailNotConfirmedException("Please confirm your email");

            var result = await VerifyPassword(user, password);

            if (!result) throw new InvalidCredentialsException("Invalid email or password");

            return user;

        }

        public async Task<object> ExecuteRegister(string firstname, string lastname, string email, string password)
        {
            // email already use by an other user?
            IUser? user = await FindUserByEmail(email);

            if (user != null) throw new EmailAlreadyUsedException($"An existing account is using {email} email address. Please try with another one");

            return await CreateUser(firstname, lastname, email, password);

        }

        private Task<IUser?> FindUserByEmail(string email)
        {
            return _accountPersistancePort.GetUserByEmail(email);
        }

        private Task<bool> VerifyPassword(IUser user, string password)
        {
            return _accountPersistancePort.VerifyPassword(user, password);
        }

        private Task<object> CreateUser(string firstname, string lastname, string email, string password)
        {
            return _accountPersistancePort.CreateUser(firstname, lastname, email, password);
        }
    }
}

