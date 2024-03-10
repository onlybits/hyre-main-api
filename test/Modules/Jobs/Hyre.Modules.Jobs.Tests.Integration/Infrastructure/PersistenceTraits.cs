// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Jobs.Tests.Integration.Infrastructure;

/// <summary>
///   Traits for integration tests of persistence.
/// </summary>
public abstract class PersistenceTraits
{
	/// <summary>
	///   Gets the name of the persistence trait.
	/// </summary>
	public const string Name = "Integration/Infrastructure/Persistence";

	/// <summary>
	///   Gets the value of the persistence trait.
	/// </summary>
	public const string Value = "Jobs";
}