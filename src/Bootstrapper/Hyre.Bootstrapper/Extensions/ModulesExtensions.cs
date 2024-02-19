// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.API;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This is an extension class that contains the configuration for the modules.
/// </summary>
public static class ModulesExtensions
{
	/// <summary>
	///   This adds the modules to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>Returns the service collection.</returns>
	public static IServiceCollection AddModulesConfiguration(this IServiceCollection services) => services
		.AddJobsModule();
}