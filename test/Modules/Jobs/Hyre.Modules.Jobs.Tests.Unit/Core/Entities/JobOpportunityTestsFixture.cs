// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Core.Enums;
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

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityDescription" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityDescription" />.</returns>
	protected JobOpportunityDescription GenerateValidDescription() => Faker.Lorem.Paragraph().ClampLength(10, 500);

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityLocation" />.
	/// </summary>
	/// <returns>If will return a valid <see cref="JobOpportunityLocation" />.</returns>
	protected JobOpportunityLocation GenerateValidLocation() => new(
		Faker.PickRandom<LocationType>(),
		Faker.Address.City().ClampLength(3, 32),
		Faker.Address.StateAbbr().ClampLength(2, 2)
	);
}