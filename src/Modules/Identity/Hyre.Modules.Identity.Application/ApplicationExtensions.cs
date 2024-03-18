// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Identity.Application;

/// <summary>
///   This class is responsible for adding the application layer to the DI container.
/// </summary>
internal static class ApplicationExtensions
{
	/// <summary>
	///   This method adds the application layer to the DI container.
	/// </summary>
	/// <param name="services">The service collection to add the application layer to.</param>
	/// <returns>The service collection with the application layer added.</returns>
	internal static IServiceCollection AddApplication(this IServiceCollection services)
	{
		_ = services.AddMediatR(cfg => _ = cfg.RegisterServicesFromAssembly(typeof(IIdentityApplicationMarker).Assembly));
		_ = services.AddScoped<IIdentityService, IdentityService>();
		return services;
	}
}