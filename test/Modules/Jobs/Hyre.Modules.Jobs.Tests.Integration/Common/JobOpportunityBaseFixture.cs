// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;

#endregion

namespace Hyre.Modules.Jobs.Tests.Integration.Common;

/// <summary>
///   The base fixture for the <see cref="JobOpportunityBaseFixture" /> class.
/// </summary>
public abstract class JobOpportunityBaseFixture : BaseFixture
{
	/// <summary>
	///   Initializes a new instance of the <see cref="JobOpportunityBaseFixture" /> class.
	/// </summary>
	/// <param name="factory">The integration tests web application factory.</param>
	protected JobOpportunityBaseFixture(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
	}

	/// <summary>
	///   Generates a <see cref="JobOpportunity" />.
	/// </summary>
	/// <returns>It will return a <see cref="JobOpportunity" />.</returns>
	protected JobOpportunity GenerateJobOpportunity() => JobOpportunity.Create(
		GenerateValidName(),
		GenerateValidDescription(),
		GenerateValidLocation(),
		GenerateValidContract(),
		GenerateListOfRequirements(5));

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

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityLocation" />.
	/// </summary>
	/// <returns>If will return a valid <see cref="JobOpportunityLocation" />.</returns>
	protected JobOpportunityLocation GenerateValidLocation() => new(
		Faker.PickRandom<LocationType>(),
		Faker.Address.City().ClampLength(3, 32),
		Faker.Address.StateAbbr().ClampLength(2, 2)
	);

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityContract" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityContract" />.</returns>
	protected JobOpportunityContract GenerateValidContract() => new(
		Faker.PickRandom<ContractType>(),
		Faker.Random.Decimal(1000, 10000),
		Faker.Random.Decimal(10000, 100000)
	);

	/// <summary>
	///   Generates a list of requirements.
	/// </summary>
	/// <param name="count">The count of requirements to generate.</param>
	/// <returns>Returns a list of requirements.</returns>
	protected JobOpportunityRequirements GenerateListOfRequirements(int count)
	{
		var values = Enumerable
			.Range(0, count)
			.Select(_ => Faker.Lorem.Word().ClampLength(3, 32))
			.ToList();

		return new JobOpportunityRequirements(values);
	}

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <param name="jobOpportunityId">The job opportunity id.</param>
	/// <returns>The valid <see cref="Candidate" />.</returns>
	protected Candidate GenerateValidCandidate(JobOpportunityId jobOpportunityId) => Candidate.Create(
		jobOpportunityId,
		GenerateValidCandidateName());

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="Candidate" />.</returns>
	private CandidateName GenerateValidCandidateName() => new(
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.LastName().ClampLength(3, 32)
	);
}