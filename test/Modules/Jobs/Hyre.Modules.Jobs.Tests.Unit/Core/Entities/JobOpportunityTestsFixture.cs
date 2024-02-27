﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.Entities;

/// <summary>
///   Fixture for the <see cref="JobOpportunityTests" /> test class.
/// </summary>
public abstract class JobOpportunityTestsFixture : BaseFixture
{
	/// <summary>
	///   Generates a valid <see cref="JobOpportunityName" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityName" />.</returns>
	protected JobOpportunityName GenerateValidName() => Faker.Name.JobTitle().ClampLength(3, 32);
}