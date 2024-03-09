// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Tests.Integration.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Application.UseCases;

/// <summary>
///   This is the fixture for the all integration tests the use cases related to the <see cref="JobOpportunity" />.
/// </summary>
public abstract class JobOpportunityUseCaseTestsFixture : JobOpportunityBaseFixture
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityUseCaseTestsFixture" /> class.
	/// </summary>
	/// <param name="factory">The factory to create the web application.</param>
	protected JobOpportunityUseCaseTestsFixture(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
	}

	/// <summary>
	///   This method is responsible for seeding the database with the given job opportunities.
	/// </summary>
	/// <param name="jobOpportunities">The job opportunities to be seeded.</param>
	protected async Task SeedDatabaseWithJobOpportunities(IEnumerable<JobOpportunity> jobOpportunities)
	{
		var contextToSeed = CreateRepositoryContext();
		await contextToSeed.JobOpportunities.AddRangeAsync(jobOpportunities, CancellationToken.None);
		_ = await contextToSeed.SaveChangesAsync(CancellationToken.None);
	}
}