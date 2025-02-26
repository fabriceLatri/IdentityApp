using System;
using Domain.Models.Account;
using Domain.Ports.Driven.Account;
using Domain.Ports.Driving.UseCases;
using Domain.Exceptions.Account;
using Domain.Ports.Driving.DTOs.Account;

namespace Domain.UseCases.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountPersistancePort _accountPersistancePort;
        private readonly IAccountAuthentificationPort _accountAuthentificationPort;

        private readonly IAccountMapperPort<IUser, IUserDto> _accountMapper;

        public AccountService(IAccountPersistancePort accountPersistancePort, IAccountAuthentificationPort accountAuthentificationPort, IAccountMapperPort<IUser, IUserDto> accountMapper)
        {
            _accountPersistancePort = accountPersistancePort;
            _accountAuthentificationPort = accountAuthentificationPort;
            _accountMapper = accountMapper;
        }


        #region public Execute methods
        public async Task<IUserDto> ExecuteLogin(string email, string password)
        {
            IUser? user = await FindUserByEmail(email) ?? throw new UserNotFoundException("Invalid email or password");

            if (!user.EmailConfirmed()) throw new EmailNotConfirmedException("Please confirm your email");

            var result = await VerifyPassword(user, password);

            if (!result) throw new InvalidCredentialsException("Invalid email or password");

            var token = _accountAuthentificationPort.CreateToken(user);

            return _accountMapper.MapTo(user, token);
        }

        public async Task<object> ExecuteRegister(string firstname, string lastname, string email, string password)
        {
            // email already use by an other user?
            IUser? user = await FindUserByEmail(email);

            if (user != null) throw new EmailAlreadyUsedException($"An existing account is using {email} email address. Please try with another one");

            return await CreateUser(firstname, lastname, email, password);

        }

        public async Task<IUserDto> ExecuteRefreshUserToken(string emailClaim)
        {
            var user = await _accountPersistancePort.RefreshUserToken(emailClaim);

            string token = _accountAuthentificationPort.CreateToken(user);

            return _accountMapper.MapTo(user, token);
        }
        #endregion

        #region private methods
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
        #endregion
    }
}

