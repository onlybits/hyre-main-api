// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Logging;
using Hyre.Shared.Infrastructure.Logging;
using Hyre.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Shared.Infrastructure;

/// <summary>
///   This class contains the extension methods for the shared infrastructure.
/// </summary>
internal static class Extensions
{
	/// <summary>
	///   This method adds the shared infrastructure to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the service collection with the shared infrastructure added.</returns>
	public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services) => services
		.AddTransient<ILoggerManager, LoggerManager>()
		.AddPostgres();

	/// <summary>
	///   This method gets the options from the registered services.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="sectionName">The section name from the configuration file.</param>
	/// <typeparam name="T">The type of the options.</typeparam>
	/// <returns>It will return the options from the configuration file.</returns>
	public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : class, new()
	{
		using var scope = services.BuildServiceProvider().CreateScope();
		var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
		return configuration.GetOptions<T>(sectionName);
	}

	/// <summary>
	///   This method gets the options from the configuration file.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="sectionName">The section name from the configuration file.</param>
	/// <typeparam name="T">The type of the options.</typeparam>
	/// <returns>It will return the options from the configuration file.</returns>
	private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
	{
		var options = new T();
		configuration.GetSection(sectionName).Bind(options);
		return options;
	}
}