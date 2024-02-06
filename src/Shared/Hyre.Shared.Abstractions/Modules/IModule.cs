// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Hyre.Shared.Abstractions.Modules;

/// <summary>
///   This is the contract for a module.
/// </summary>
public interface IModule
{
	/// <summary>
	///   Gets the name of the module.
	/// </summary>
	string Name { get; }


	/// <summary>
	///   This method registers the module's services.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the same service collection.</returns>
	IServiceCollection Register(IServiceCollection services);

	/// <summary>
	///   This method configures the module's services.
	/// </summary>
	/// <param name="app">The application builder.</param>
	/// <returns>It will return the same application builder.</returns>
	IApplicationBuilder Use(IApplicationBuilder app);
}