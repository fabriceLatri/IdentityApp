using System;
namespace Domain.Models.Account
{
	public interface IUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool EmailConfirmed();


	}
}
