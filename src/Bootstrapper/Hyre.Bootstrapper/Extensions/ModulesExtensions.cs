// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.API;
using Hyre.Modules.Notifications.API;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This extension method is used to configure all modules in the bootstrapper.
/// </summary>
internal static class ModulesExtensions
{
	/// <summary>
	///   Use this method to configure all modules in the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the service collection.</returns>
	public static IServiceCollection AddModulesConfiguration(this IServiceCollection services) => services
		.RegisterJobsModule()
		.RegisterNotificationsModule();

	/// <summary>
	///   This method is used to configure all modules in the application builder.
	/// </summary>
	/// <param name="app">The application builder.</param>
	/// <returns>It will return the application builder.</returns>
	public static IApplicationBuilder UseModules(this IApplicationBuilder app) => app
		.UseJobsModule()
		.UseNotificationsModule();
}