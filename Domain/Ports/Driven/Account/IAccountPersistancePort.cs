using System;
using Domain.Models.Account;

namespace Domain.Ports.Driven.Account
{
	public interface IAccountPersistancePort
	{
		public Task<IUser?> GetUserByEmail(string email);

		public Task<bool> VerifyPassword(IUser user, string password);
	}
}

