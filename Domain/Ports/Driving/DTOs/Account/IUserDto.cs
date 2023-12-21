namespace Domain.Ports.Driving.DTOs.Account
{
	public interface IUserDto
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Token { get; set; }
	}
}

