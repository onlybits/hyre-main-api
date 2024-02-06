// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Reflection;
using Hyre.Shared.Abstractions.Modules;
using Hyre.Shared.Infrastructure.Modules;

#endregion

namespace Hyre.Bootstrapper.Extensions;

/// <summary>
///   This extension method is used to configure all modules in the bootstrapper.
/// </summary>
internal static class ModulesExtensions
{
	private static IList<Assembly>? _assemblies;
	private static IList<IModule>? _modules;

	/// <summary>
	///   Use this method to configure all modules in the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>It will return the service collection.</returns>
	public static IServiceCollection AddModulesConfiguration(this IServiceCollection services)
	{
		_assemblies = ModuleLoader.LoadAssemblies();
		_modules = ModuleLoader.LoadModules(_assemblies);

		if (_modules is null)
		{
			return services;
		}

		foreach (var module in _modules)
		{
			_ = module.Register(services);
		}

		return services;
	}

	/// <summary>
	///   This method is used to configure all modules in the application builder.
	/// </summary>
	/// <param name="app">The application builder.</param>
	/// <returns>It will return the application builder.</returns>
	public static IApplicationBuilder UseModules(this IApplicationBuilder app)
	{
		if (_assemblies is null || _modules is null)
		{
			return app;
		}

		foreach (var module in _modules)
		{
			_ = module.Use(app);
		}

		_assemblies.Clear();
		_modules.Clear();
		return app;
	}
}