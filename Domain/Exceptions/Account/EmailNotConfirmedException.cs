using System;
namespace Domain.Exceptions.Account
{
	public class EmailNotConfirmedException : Exception
	{
		public EmailNotConfirmedException(string message) : base(message)
		{
		}
	}
}

