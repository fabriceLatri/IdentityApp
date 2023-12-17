using System;
using Domain.Ports.Driven.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DrivenAdapters.DatabaseAdapters.Configuration
{
	public static class DatabaseAdapterConfiguration
	{
		public static IServiceCollection AddDatabase(this IServiceCollection services, string databaseConnection)
		{
			services.AddDbContext<Context>(options => options.UseSqlServer(databaseConnection));

			services.AddTransient<IAccountPersistancePort, AccountPersistanceAdapter>();

			return services;
		}
	}
}

