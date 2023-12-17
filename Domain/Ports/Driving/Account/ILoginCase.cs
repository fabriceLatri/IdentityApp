using System;
using Domain.Models.Account;

namespace Domain.Ports.Driving.Account
{
	public interface ILoginCase
	{
        Task<IUser> Execute(String email, String password);

    }
}

