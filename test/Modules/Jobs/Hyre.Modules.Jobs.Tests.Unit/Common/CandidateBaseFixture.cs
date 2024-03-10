// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Find;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Update;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Common;

/// <summary>
///   The base fixture for the <see cref="Candidate" /> class.
/// </summary>
public abstract class CandidateBaseFixture : BaseFixture
{
	#region UseCases - Find

	/// <summary>
	///   Generates a valid <see cref="FindCandidateRequest" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="FindCandidateRequest" />.</returns>
	protected FindCandidateRequest GenerateFindCandidateRequest() => new(
		JobOpportunityId.New(),
		CandidateId.New(),
		false);

	#endregion

	#region UseCases - List

	/// <summary>
	///   Generates a valid paged list of <see cref="Candidate" />.
	/// </summary>
	/// <param name="pageSize">The page size.</param>
	/// <param name="pageNumber">The current page number.</param>
	/// <param name="totalCount">The total number of items.</param>
	/// <returns>Returns a paged list of <see cref="Candidate" />.</returns>
	protected PagedList<Candidate> GeneratePagedListCandidates(int pageSize, int pageNumber, int totalCount) => new(
		GenerateValidListOfCandidates(totalCount),
		totalCount,
		pageNumber,
		pageSize);

	/// <summary>
	///   Generates a valid list of <see cref="Candidate" />.
	/// </summary>
	/// <param name="count">The count of candidates to generate.</param>
	/// <returns>Returns a valid list of <see cref="Candidate" />.</returns>
	private List<Candidate> GenerateValidListOfCandidates(int count) =>
		Enumerable
			.Range(0, count)
			.Select(_ => GenerateValidCandidate())
			.ToList();

	#endregion

	#region Core Entity

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>The valid <see cref="Candidate" />.</returns>
	protected Candidate GenerateValidCandidate() => Candidate.Create(
		JobOpportunityId.New(),
		GenerateValidName(),
		GenerateCandidateEmail());

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="Candidate" />.</returns>
	protected CandidateName GenerateValidName() => new(
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

	#endregion

	#region UseCases - Create

	/// <summary>
	///   Generates a valid <see cref="CreateCandidateInput" />.
	/// </summary>
	/// <returns></returns>
	private CreateCandidateInput GenerateCreateCandidateInput() => new(
		GenerateValidName(),
		GenerateCandidateEmail());

	/// <summary>
	///   Generates a valid <see cref="CreateCandidateRequest" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CreateCandidateRequest" />.</returns>
	protected CreateCandidateRequest GenerateCreateCandidateRequest() => new(
		JobOpportunityId.New(),
		GenerateCreateCandidateInput(),
		false);

	#endregion

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

	#region UseCases - Update

	protected UpdateCandidateRequest GenerateUpdateCandidateRequest() => new(
		JobOpportunityId.New(),
		CandidateId.New(),
		GenerateUpdateCandidateInput(),
		false);

	/// <summary>
	///   Generates a valid <see cref="UpdateCandidateInput" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="UpdateCandidateInput" />.</returns>
	private UpdateCandidateInput GenerateUpdateCandidateInput() => new(GenerateValidName());

	#endregion
}