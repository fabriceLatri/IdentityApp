using System;
namespace Domain.Models.Account
{
	public interface IUser
	{
		public string Id { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool EmailConfirmed();
	}
}
