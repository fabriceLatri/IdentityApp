using System;
namespace Domain.Exceptions.Account
{
	public class InvalidCredentialsException : Exception
	{
		public InvalidCredentialsException(string message) : base(message)
		{
		}
	}
}

