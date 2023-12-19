using System;
using Domain.Models.Account;

namespace Domain.Ports.Driving.Account
{
	public interface IAccountCase
	{
        Task<IUser> ExecuteLogin(string email, string password);

        Task<object> ExecuteRegister(string firstname, string lastname, string email, string password);

        Task<IUser> ExecuteRefreshUserToken(string emailClaim);
    }
}

