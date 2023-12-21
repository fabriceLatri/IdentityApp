using Domain.Ports.Driving.DTOs.Account;
namespace Api.DrivingAdapters.DTOs.Account
{
	public class UserDto : IUserDto
	{
		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Token { get; set; }

		public UserDto()
		{
		}
	}
}

