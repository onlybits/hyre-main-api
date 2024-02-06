// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Reflection;
using Hyre.Shared.Abstractions.Exceptions;
using Hyre.Shared.Abstractions.Modules;

#endregion

namespace Hyre.Shared.Infrastructure.Modules;

/// <summary>
///   This class is responsible for loading the assemblies and the modules from the assemblies
/// </summary>
public static class ModuleLoader
{
	/// <summary>
	///   This method will load all the assemblies from the current domain and the subdirectories
	/// </summary>
	/// <returns>Returns a list of assemblies</returns>
	public static IList<Assembly>? LoadAssemblies()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
		var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
		var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)
			.Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
			.ToList();

		files.ForEach(file => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file))));

		return assemblies;
	}

	/// <summary>
	///   This method will load all the modules from the assemblies
	/// </summary>
	/// <param name="assemblies">The assemblies to load the modules from</param>
	/// <returns>Returns a list of modules</returns>
	public static IList<IModule>? LoadModules(IEnumerable<Assembly>? assemblies)
	{
		if (assemblies is null)
		{
			throw new EmptyAssembliesException();
		}


		var modules = assemblies.SelectMany(x => x.GetTypes())
			.Where(x => typeof(IModule).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
			.OrderBy(x => x.Name)
			.Select(Activator.CreateInstance)
			.Cast<IModule>()
			.ToList();

		return modules;
	}
}

/// <summary>
///   Exception that is thrown when the assemblies list is empty
/// </summary>
internal sealed class EmptyAssembliesException : ServerFailureException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="EmptyAssembliesException" /> class.
	/// </summary>
	public EmptyAssembliesException() : base("The assemblies list is empty")
	{
	}

	//TODO: Add translation to the exception message
}