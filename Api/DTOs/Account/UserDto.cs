using System;
namespace Api.DTOs.Account
{
	public class UserDto
	{
		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string JWT { get; set; }

		public UserDto()
		{
		}
	}
}

