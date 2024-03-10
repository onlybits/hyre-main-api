// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus;

#endregion

namespace Hyre.Modules.Notifications.Tests.Unit.Common;

/// <summary>
///   The base class for all test fixtures.
/// </summary>
public abstract class BaseFixture
{
	/// <summary>
	///   The instance of <see cref="Faker" /> to generate random data.
	/// </summary>
	protected Faker Faker { get; } = new("pt_BR");
}