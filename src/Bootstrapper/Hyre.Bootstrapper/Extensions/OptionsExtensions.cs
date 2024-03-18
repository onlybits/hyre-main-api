// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Core.Options;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   Contains extension methods for configuring options.
/// </summary>
public static class OptionsExtensions
{
	/// <summary>
	///   Configures the options for JWT authentication.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="configuration">The configuration instance.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration) => services
		.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Name));
}