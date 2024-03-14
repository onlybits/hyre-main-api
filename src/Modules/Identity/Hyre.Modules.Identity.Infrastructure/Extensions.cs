// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Identity.Infrastructure;

/// <summary>
///   This class contains the extension methods for the infrastructure layer.
/// </summary>
internal static class Extensions
{
	/// <summary>
	///   This method adds the infrastructure layer to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the service collection.</returns>
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		_ = services.AddPostgres<IdentityRepositoryContext>();

		return services;
	}
}