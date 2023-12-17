using System;
namespace Api.DrivingAdapters.DTOs.Account
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

