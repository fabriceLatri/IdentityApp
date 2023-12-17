using System;
using Api.DrivenAdapters.Entities.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.DrivenAdapters.DatabaseAdapters
{
	public class Context : IdentityDbContext<User>
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
	}
}

