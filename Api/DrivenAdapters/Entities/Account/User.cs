using System;
using Domain.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace Api.DrivenAdapters.Entities.Account

{
	public class User : IdentityUser, IUser
    {
        public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;

        bool IUser.EmailConfirmed()
        {
            return EmailConfirmed;
        }
    }
}

