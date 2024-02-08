// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This class is used to configure the CQRS pattern.
/// </summary>
public static class CqrsExtensions
{
	/// <summary>
	///   This method is used to configure the <see cref="MediatR" /> package.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the same service collection.</returns>
	public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
	{
		_ = services.AddMediatR(cfg => _ = cfg.RegisterServicesFromAssembly(typeof(IJobsApplicationMarker).Assembly));

		return services;
	}
}