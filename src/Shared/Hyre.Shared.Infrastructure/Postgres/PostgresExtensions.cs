// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Shared.Infrastructure.Postgres;

/// <summary>
///   This class contains the extension methods for the Postgres context.
/// </summary>
public static class PostgresExtensions
{
	/// <summary>
	///   This method is used to register the Postgres context from the modules.
	/// </summary>
	/// <param name="services">The module's service collection.</param>
	/// <typeparam name="T">The type of the Postgres context.</typeparam>
	/// <returns>It will return the service collection with the Postgres context added.</returns>
	public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
	{
		var options = services.GetOptions<PostgresOptions>(PostgresOptions.SectionName);
		services.AddDbContext<T>(opt => opt.UseNpgsql(options.ConnectionString).UseSnakeCaseNamingConvention());
		return services;
	}

	/// <summary>
	///   This method internally configures the Postgres options.
	/// </summary>
	/// <param name="services">The internal service collection.</param>
	/// <returns>It will return the service collection with the Postgres options added.</returns>
	internal static IServiceCollection AddPostgres(this IServiceCollection services)
	{
		var options = services.GetOptions<PostgresOptions>(PostgresOptions.SectionName);
		services.AddSingleton(options);
		services.AddHostedService<DbContextAppInitializer>();
		return services;
	}
}