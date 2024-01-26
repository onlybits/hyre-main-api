// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Integration.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Infrastructure.Persistence;

/// <summary>
///   This is the fixture for the <see cref="JobOpportunityRepositoryTests" />.
/// </summary>
public abstract class JobOpportunityRepositoryTestsFixture : BaseFixture
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityRepositoryTestsFixture" /> class.
	/// </summary>
	/// <param name="factory">The factory to create the web application.</param>
	protected JobOpportunityRepositoryTestsFixture(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
	}

	/// <summary>
	///   Generates a list of <see cref="JobOpportunity" />.
	/// </summary>
	/// <param name="count">The number of <see cref="JobOpportunity" /> to generate.</param>
	/// <returns>It will return a list of <see cref="JobOpportunity" />.</returns>
	protected IList<JobOpportunity> GenerateJobOpportunities(int count) => Enumerable
		.Range(1, count)
		.Select(_ => GenerateJobOpportunity())
		.ToList();

	/// <summary>
	///   Generates a <see cref="JobOpportunity" />.
	/// </summary>
	/// <returns>It will return a <see cref="JobOpportunity" />.</returns>
	protected JobOpportunity GenerateJobOpportunity() => JobOpportunity.Create(GenerateJobOpportunityName());

	/// <summary>
	///   Generates a <see cref="JobOpportunityName" /> with a random name.
	/// </summary>
	/// <returns>It will return a <see cref="JobOpportunityName" /> with a random name.</returns>
	private JobOpportunityName GenerateJobOpportunityName() => new(Faker.Name.JobTitle().ClampLength(3, 32));
}