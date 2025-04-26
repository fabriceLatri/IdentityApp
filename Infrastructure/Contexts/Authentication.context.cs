using Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AuthenticationContext(DbContextOptions<AuthenticationContext> options) : IdentityDbContext<UserModel>(options)
	{
}