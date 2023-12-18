using System;
namespace Domain.Exceptions.Account
{
	public class UserCreateException : Exception
	{
		public UserCreateException(IEnumerable<object> errors) : base()
		{
		}
	}
}

