using System;
namespace Domain.Ports.Driving.DTOs.Account
{
	public interface IUserDto
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}

