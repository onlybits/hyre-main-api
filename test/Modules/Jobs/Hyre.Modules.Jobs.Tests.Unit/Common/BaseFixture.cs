// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Common;

/// <summary>
///   The base fixture for the unit tests.
/// </summary>
public abstract class BaseFixture
{
	/// <summary>
	///   Gets the Bogus faker instance.
	/// </summary>
	public Faker Faker { get; } = new("pt_BR");
}