using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DrivingAdapters.DTOs.Account
{
	public class LoginDto
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

        public LoginDto()
		{
		}
	}
}

