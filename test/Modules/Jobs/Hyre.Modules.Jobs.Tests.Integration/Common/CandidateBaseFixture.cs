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
///   The base fixture for the <see cref="CandidateBaseFixture" /> class.
/// </summary>
public abstract class CandidateBaseFixture : BaseFixture
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CandidateBaseFixture" /> class.
	/// </summary>
	/// <param name="factory">The integration tests web application factory.</param>
	protected CandidateBaseFixture(IntegrationTestsWebApplicationFactory factory) : base(factory)
	{
	}

	/// <summary>
	///   Generates a list of <see cref="Candidate" />.
	/// </summary>
	/// <param name="count">The number of <see cref="Candidate" /> to generate.</param>
	/// <returns>Returns a list of <see cref="Candidate" />.</returns>
	protected IEnumerable<Candidate> GenerateValidCandidates(int count) => Enumerable
		.Range(1, count)
		.Select(_ => GenerateValidCandidate())
		.ToList();

	/// <summary>
	///   Generates a list of <see cref="Candidate" /> with a given <see cref="JobOpportunityId" />.
	/// </summary>
	/// <param name="count">The number of <see cref="Candidate" /> to generate.</param>
	/// <param name="jobOpportunityId">The <see cref="JobOpportunityId" /> to associate with the <see cref="Candidate" />.</param>
	/// <returns>Returns a list of <see cref="Candidate" />.</returns>
	protected ICollection<Candidate> GenerateCandidatesWithJobOpportunity(int count, JobOpportunityId jobOpportunityId) => Enumerable
		.Range(1, count)
		.Select(_ => GenerateCandidateWithJobOpportunity(jobOpportunityId))
		.ToList();

	/// <summary>
	///   Generates a <see cref="Candidate" /> with a given <see cref="JobOpportunityId" />.
	/// </summary>
	/// <param name="jobOpportunityId">The <see cref="JobOpportunityId" /> to associate with the <see cref="Candidate" />.</param>
	/// <returns>Returns a <see cref="Candidate" />.</returns>
	protected Candidate GenerateCandidateWithJobOpportunity(JobOpportunityId jobOpportunityId) => Candidate.Create(
		jobOpportunityId,
		GenerateCandidateName(),
		GenerateCandidateEmail());

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="Candidate" />.</returns>
	private Candidate GenerateValidCandidate() => Candidate.Create(
		JobOpportunityId.New(),
		GenerateCandidateName(),
		GenerateCandidateEmail());

	/// <summary>
	///   Generates a valid <see cref="CandidateName" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateName" />.</returns>
	protected CandidateName GenerateCandidateName() => new(
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.LastName().ClampLength(3, 32)
	);

	/// <summary>
	///   Generates a valid <see cref="CandidateEmail" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateEmail" />.</returns>
	protected CandidateEmail GenerateCandidateEmail() => new(
		Faker.Internet.Email());

	/// <summary>
	///   This method will seed the database with the given <see cref="JobOpportunity" /> and a list of
	///   <see cref="Candidate" />.
	/// </summary>
	/// <param name="jobOpportunity">The <see cref="JobOpportunity" /> to seed the database with.</param>
	/// <param name="candidates">The <see cref="Candidate" /> to seed the database with.</param>
	protected async Task SeedDatabaseAsync(JobOpportunity? jobOpportunity = null, IEnumerable<Candidate>? candidates = null)
	{
		var context = CreateRepositoryContext();
		if (jobOpportunity is not null)
		{
			await context.JobOpportunities.AddRangeAsync(jobOpportunity);
		}

		if (candidates is not null)
		{
			await context.Candidates.AddRangeAsync(candidates);
		}

		_ = await context.SaveChangesAsync();

		// I need to clear the change tracker to avoid conflicts with the next test.
		context.ChangeTracker.Clear();
	}

	#region Core Entity - JobOpportunity

	/// <summary>
	///   Generates a valid <see cref="JobOpportunity" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunity" />.</returns>
	protected JobOpportunity GenerateValidJobOpportunity() => JobOpportunity.Create(
		GenerateValidJobOpportunityName(),
		GenerateValidJobOpportunityDescription(),
		GenerateValidJobOpportunityLocation(),
		GenerateValidJobOpportunityContract(),
		GenerateListOfRequirements(5)
	);

	/// <summary>
	///   Generates a valid name for the <see cref="JobOpportunityName" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityName" />.</returns>
	private JobOpportunityName GenerateValidJobOpportunityName() => new(Faker.Name.JobTitle().ClampLength(3, 64));

	/// <summary>
	///   Generates a valid description for the <see cref="JobOpportunityDescription" /> class.
	/// </summary>
	/// <returns>I will return a valid <see cref="JobOpportunityDescription" />.</returns>
	private JobOpportunityDescription GenerateValidJobOpportunityDescription() => new(Faker.Lorem.Paragraph().ClampLength(10, 500));

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityLocation" />.
	/// </summary>
	/// <returns>If will return a valid <see cref="JobOpportunityLocation" />.</returns>
	private JobOpportunityLocation GenerateValidJobOpportunityLocation() => new(
		Faker.PickRandom<LocationType>(),
		Faker.Address.City().ClampLength(3, 32),
		Faker.Address.StateAbbr().ClampLength(2, 2)
	);

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityContract" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityContract" />.</returns>
	private JobOpportunityContract GenerateValidJobOpportunityContract() => new(
		Faker.PickRandom<ContractType>(),
		Faker.Random.Decimal(1000, 10000),
		Faker.Random.Decimal(10000, 100000)
	);

	/// <summary>
	///   Generates a list of requirements.
	/// </summary>
	/// <param name="count">The count of requirements to generate.</param>
	/// <returns>Returns a list of requirements.</returns>
	private JobOpportunityRequirements GenerateListOfRequirements(int count)
	{
		var values = Enumerable
			.Range(0, count)
			.Select(_ => Faker.Lorem.Word().ClampLength(3, 32))
			.ToList();

		return new JobOpportunityRequirements(values);
	}

	#endregion
}