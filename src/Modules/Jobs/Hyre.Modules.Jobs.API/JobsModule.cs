// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application;
using Hyre.Modules.Jobs.Infrastructure;
using Hyre.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Modules.Jobs.API;

/// <summary>
///   This is the jobs module configuration.
/// </summary>
/// <inheritdoc cref="IModule" />
internal sealed class JobsModule : IModule
{
	/// <summary>
	///   Gets the name of the module.
	/// </summary>
	public string Name => "Jobs";

	/// <summary>
	///   This method is responsible for registering the services of the module.
	/// </summary>
	/// <param name="services">The services collection.</param>
	public IServiceCollection Register(IServiceCollection services) => services
		.AddApplication()
		.AddInfrastructure();

	/// <summary>
	///   This method is responsible for configuring the module.
	/// </summary>
	/// <param name="app">The application builder.</param>
	/// <returns>Returns the application builder.</returns>
	public IApplicationBuilder Use(IApplicationBuilder app) => app;
}