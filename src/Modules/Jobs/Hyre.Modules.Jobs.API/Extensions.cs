// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Jobs.API;

/// <summary>
///   Internal extensions for the Jobs module.
/// </summary>
internal static class Extensions
{
	/// <summary>
	///   Adds the Jobs module to the service collection.
	/// </summary>
	/// <param name="services">The main service collection.</param>
	/// <returns>It will return the same service collection.</returns>
	internal static IServiceCollection AddJobsModule(this IServiceCollection services) => services;

	/// <summary>
	///   Adds the Jobs module to the application builder.
	/// </summary>
	/// <param name="app">The main application builder.</param>
	/// <returns>It will return the same application builder.</returns>
	internal static IApplicationBuilder UseJobsModule(this IApplicationBuilder app) => app;
}