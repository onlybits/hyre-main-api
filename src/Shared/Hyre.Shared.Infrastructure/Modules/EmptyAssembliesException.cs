// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Shared.Infrastructure.Modules;

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