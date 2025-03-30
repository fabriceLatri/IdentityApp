using Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AuthenticationContext  : IdentityDbContext<UserModel>
	{
		public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
		{
		}
	}