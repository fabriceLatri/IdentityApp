using System;
using Microsoft.AspNetCore.Identity;
namespace Api.Models
{
	public class User : IdentityUser
    {
        public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
	}
}

