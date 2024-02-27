// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Jobs.Core;

internal static class Extensions
{
	internal static IServiceCollection AddJobsCore(this IServiceCollection services) => services;
}