// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Jobs.API;

internal static class Extensions
{
	internal static IServiceCollection AddJobsModule(this IServiceCollection services) => services
		.AddJobsCore();

	internal static IApplicationBuilder UseJobsModule(this IApplicationBuilder app) => app;
}