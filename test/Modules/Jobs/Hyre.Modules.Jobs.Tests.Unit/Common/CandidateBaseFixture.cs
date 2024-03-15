// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Globalization;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
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
	private IEnumerable<Candidate> GenerateValidListOfCandidates(int count) => Enumerable
		.Range(0, count)
		.Select(_ => GenerateCandidate(new List<JobOpportunity> { GenerateJobOpportunity() }))
		.AsEnumerable();

	#endregion

	#region Core Entity - Candidate

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>The valid <see cref="Candidate" />.</returns>
	protected Candidate GenerateCandidate(ICollection<JobOpportunity> jobOpportunities) => Candidate.Create(
		GenerateCandidateName(),
		GenerateCandidateEmail(),
		GenerateCandidateDocument(),
		GenerateCandidateDateOfBirth(),
		GenerateCandidateSeniority(),
		GenerateCandidateDisability(),
		GenerateCandidateGender(),
		GenerateCandidatePhoneNumber(),
		GenerateCandidateAddress(),
		GenerateCandidateEducation(2),
		GenerateCandidateExperience(3).ToList(),
		jobOpportunities,
		GenerateCandidateSocialNetwork(),
		GenerateCandidateLanguage(2).ToList());

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="Candidate" />.</returns>
	protected CandidateName GenerateCandidateName() => new(
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.LastName().ClampLength(3, 32));

	/// <summary>
	///   Generates a valid <see cref="CandidateEmail" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateEmail" />.</returns>
	protected CandidateEmail GenerateCandidateEmail() => new(
		Faker.Internet.Email());

	/// <summary>
	///   Generates a valid <see cref="CandidateDocument" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateDocument" />.</returns>
	protected CandidateDocument GenerateCandidateDocument() => new(
		Faker.Person.Cpf());

	/// <summary>
	///   Generates a valid <see cref="CandidateDateOfBirth" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateDateOfBirth" />.</returns>
	protected CandidateDateOfBirth GenerateCandidateDateOfBirth() => new(
		DateOnly.FromDateTime(Faker.Date.Past(16, DateTime.Now.AddYears(-60))));

	/// <summary>
	///   Generates a valid <see cref="CandidateSeniority" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateSeniority" />.</returns
	protected CandidateSeniority GenerateCandidateSeniority() => new(
		Faker.PickRandom<ExperienceLevel>());

	/// <summary>
	///   Generates a valid <see cref="CandidateDisability" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateDisability" />.</returns>
	protected CandidateDisability GenerateCandidateDisability() => new(
		Faker.Random.Bool(),
		Faker.Random.Bool(),
		Faker.Random.Bool(),
		Faker.Random.Bool());

	/// <summary>
	///   Generates a valid <see cref="CandidateGender" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateGender" />.</returns>
	protected CandidateGender GenerateCandidateGender() => new(
		Faker.PickRandom<Gender>());

	/// <summary>
	///   Generates a valid <see cref="CandidatePhoneNumber" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidatePhoneNumber" />.</returns>
	protected CandidatePhoneNumber GenerateCandidatePhoneNumber() => new(
		Faker.Random.Number(10, 99).ToString(CultureInfo.InvariantCulture),
		Faker.Random.Number(900000000, 999999999).ToString(CultureInfo.InvariantCulture));

	/// <summary>
	///   Generates a valid <see cref="CandidateAddress" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateAddress" />.</returns>
	protected CandidateAddress GenerateCandidateAddress() => new(
		Faker.Address.Country(),
		Faker.Address.StateAbbr(),
		Faker.Address.City().ClampLength(3, 32),
		Faker.Address.StreetName().ClampLength(3, 64),
		Faker.Address.Direction(),
		Convert.ToInt32(Faker.Address.BuildingNumber(), CultureInfo.InvariantCulture),
		Faker.Address.ZipCode().ClampLength(8, 8));

	/// <summary>
	///   Generates a valid <see cref="CandidateEducation" />.
	/// </summary>
	/// <param name="count">The count of languages to generate.</param>
	/// <returns>Returns a valid <see cref="CandidateEducation" />.</returns>
	protected ICollection<CandidateEducation> GenerateCandidateEducation(int count) => Enumerable
		.Range(1, count)
		.Select(x => new CandidateEducation(
			DateOnly.FromDateTime(Faker.Date.Past()),
			DateOnly.FromDateTime(Faker.Date.Past()),
			Faker.Company.CompanyName(),
			Faker.Lorem.Paragraph().ClampLength(10, 500),
			Faker.PickRandom<Degree>()))
		.ToList();

	/// <summary>
	///   Generates a valid <see cref="CandidateExperience" />.
	/// </summary>
	/// <param name="count">The count of languages to generate.</param>
	/// <returns>Returns a valid <see cref="CandidateExperience" />.</returns>
	protected IEnumerable<CandidateExperience> GenerateCandidateExperience(int count) => Enumerable
		.Range(1, count)
		.Select(_ => new CandidateExperience(
			DateOnly.FromDateTime(Faker.Date.Past()),
			DateOnly.FromDateTime(Faker.Date.Past()),
			Faker.Name.JobTitle(),
			Faker.Company.CompanyName(),
			Faker.Name.JobDescriptor()))
		.AsEnumerable();

	/// <summary>
	///   Generates a valid <see cref="CandidateLanguage" />.
	/// </summary>
	/// <param name="count">The count of languages to generate.</param>
	/// <returns>Returns a valid <see cref="CandidateLanguage" />.</returns>
	protected IEnumerable<CandidateLanguage> GenerateCandidateLanguage(int count) =>
		Enumerable.Range(1, count)
			.Select(x => new CandidateLanguage(
				Faker.Lorem.Word(),
				Faker.PickRandom<LanguageProficiency>()))
			.AsEnumerable();

	/// <summary>
	///   Generates a valid <see cref="CandidateSocialNetwork" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateSocialNetwork" />.</returns>
	protected CandidateSocialNetwork GenerateCandidateSocialNetwork() => new(
		Faker.Internet.Url(),
		Faker.Internet.Url());

	#endregion

	#region UseCases - Create

	/// <summary>
	///   Generates a valid <see cref="CreateCandidateInput" />.
	/// </summary>
	/// <returns></returns>
	private CreateCandidateInput GenerateCreateCandidateInput() => new(
		GenerateCandidateName(),
		GenerateCandidateEmail(),
		GenerateCandidateDocument(),
		GenerateCandidateDateOfBirth(),
		GenerateCandidateSeniority(),
		GenerateCandidateDisability(),
		GenerateCandidateGender(),
		GenerateCandidatePhoneNumber(),
		GenerateCandidateAddress(),
		GenerateCandidateEducation(2),
		GenerateCandidateExperience(3).ToList(),
		GenerateCandidateSocialNetwork(),
		GenerateCandidateLanguage(2).ToList());

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
	protected JobOpportunity GenerateJobOpportunity() => JobOpportunity.Create(
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
	private UpdateCandidateInput GenerateUpdateCandidateInput() => new(GenerateCandidateName());

	#endregion
}