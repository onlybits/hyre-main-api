// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Notifications.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Notifications.API;

/// <summary>
///   This is the notifications module configuration.
/// </summary>
internal static class NotificationsModule
{
	/// <summary>
	///   This method registers the notifications module to the main bootstrapper.
	/// </summary>
	/// <param name="services">The services collection from the main bootstrapper.</param>
	/// <returns>Returns the services collection with the notifications module registered.</returns>
	public static IServiceCollection RegisterNotificationsModule(this IServiceCollection services) => services
		.AddInfrastructure();

	/// <summary>
	///   This method registers the notifications module to the main bootstrapper.
	/// </summary>
	/// <param name="app">The application builder from the main bootstrapper.</param>
	/// <returns>Returns the application builder with the notifications module registered.</returns>
	public static IApplicationBuilder UseNotificationsModule(this IApplicationBuilder app) => app;
}