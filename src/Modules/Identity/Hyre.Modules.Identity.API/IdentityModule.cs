// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application;
using Hyre.Modules.Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Identity.API;

/// <summary>
///   This class contains the extensions for the identity module.
/// </summary>
public static class IdentityModule
{
	/// <summary>
	///   This method is responsible for registering the services of the module.
	/// </summary>
	/// <param name="services">The services collection.</param>
	public static IServiceCollection RegisterIdentityModule(this IServiceCollection services) => services
		.AddApplication()
		.AddInfrastructure();

	/// <summary>
	///   This method is responsible for configuring the module.
	/// </summary>
	/// <param name="app">The application builder.</param>
	/// <returns>Returns the application builder.</returns>
	public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app) => app;
}