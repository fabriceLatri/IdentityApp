using System;
using Domain.Ports.Driving.Account;
using Domain.UseCases.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DrivingAdapters.Configuration
{
	public static class UseCaseConfiguration
	{
		public static IServiceCollection AddUseCases(this IServiceCollection services)
		{
			services.AddTransient<ILoginCase, LoginCase>();

			return services;
		}
	}
}

