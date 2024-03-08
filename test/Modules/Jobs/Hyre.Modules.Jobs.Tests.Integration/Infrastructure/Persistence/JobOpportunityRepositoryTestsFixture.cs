// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Tests.Integration.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Infrastructure.Persistence;

/// <summary>
///   This is the fixture for the <see cref="JobOpportunityRepositoryTests" />.
/// </summary>
public abstract class JobOpportunityRepositoryTestsFixture : JobOpportunityBaseFixture
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
	protected ICollection<JobOpportunity> GenerateJobOpportunities(int count) => Enumerable
		.Range(1, count)
		.Select(_ => GenerateJobOpportunity())
		.ToList();

	/// <summary>
	///   Generates a <see cref="JobOpportunity" /> with candidates.
	/// </summary>
	/// <returns>Returns a <see cref="JobOpportunity" /> with candidates.</returns>
	protected JobOpportunity GenerateJobOpportunityWithCandidates()
	{
		var jobOpportunity = GenerateJobOpportunity();
		var candidate = GenerateValidCandidate(jobOpportunity.Id);

		jobOpportunity.AddCandidate(candidate);

		return jobOpportunity;
	}
}