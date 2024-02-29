// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Logging;
using Hyre.Shared.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Shared.Infrastructure;

/// <summary>
///   This class contains the extension methods for the shared infrastructure.
/// </summary>
internal static class Extensions
{
	/// <summary>
	///   This method adds the shared infrastructure to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the service collection with the shared infrastructure added.</returns>
	public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services) => services
		.AddTransient<ILoggerManager, LoggerManager>();
}