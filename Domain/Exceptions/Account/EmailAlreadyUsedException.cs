using System;
namespace Domain.Exceptions.Account
{
	public class EmailAlreadyUsedException : Exception
	{
		public EmailAlreadyUsedException(string message) : base(message)
		{
		}
	}
}

