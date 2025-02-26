using Domain.Ports.Driving.UseCases;
using Domain.UseCases.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DrivingAdapters.Configuration
{
	public static class UseCaseConfiguration
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IAccountService, AccountService>();

			return services;
		}
	}
}

