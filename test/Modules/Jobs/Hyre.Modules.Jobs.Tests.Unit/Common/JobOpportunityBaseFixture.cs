// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Common;

/// <summary>
///   The base fixture for the <see cref="JobOpportunityBaseFixture" /> class.
/// </summary>
public abstract class JobOpportunityBaseFixture : BaseFixture
{
	/// <summary>
	///   Generates a valid name for the <see cref="JobOpportunityName" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityName" />.</returns>
	protected JobOpportunityName GenerateValidName() => new(Faker.Name.JobTitle().ClampLength(3, 32));

	/// <summary>
	///   Generates a valid description for the <see cref="JobOpportunityDescription" /> class.
	/// </summary>
	/// <returns>I will return a valid <see cref="JobOpportunityDescription" />.</returns>
	protected JobOpportunityDescription GenerateValidDescription() => new(Faker.Lorem.Paragraph().ClampLength(10, 500));
}