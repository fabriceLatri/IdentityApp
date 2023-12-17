using System;
namespace Domain.Exceptions.Account
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string message) : base(message)
		{
        }
	}
}

