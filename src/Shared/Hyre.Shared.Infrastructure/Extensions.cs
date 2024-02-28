// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Logging;
using Hyre.Shared.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Shared.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
	{
		// Register the logger manager
		services.AddTransient(typeof(ILoggerManager<>), typeof(LoggerManager<>));
		return services;
	}
}