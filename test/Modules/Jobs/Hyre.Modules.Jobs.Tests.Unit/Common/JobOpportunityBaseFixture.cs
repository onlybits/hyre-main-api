// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Globalization;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Create;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Find;
using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Requests;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Common;

/// <summary>
///   The base fixture for the <see cref="JobOpportunityBaseFixture" /> class.
/// </summary>
public abstract class JobOpportunityBaseFixture : BaseFixture
{
	#region UseCases - Delete

	/// <summary>
	///   Generates a valid request for the <see cref="DeleteJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="DeleteJobOpportunityRequest" />.</returns>
	protected DeleteJobOpportunityRequest GenerateDeleteJobOpportunityRequest() => new(
		JobOpportunityId.New(),
		false);

	#endregion

	#region UseCases - Find

	/// <summary>
	///   Generates a valid request for the <see cref="FindJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="FindJobOpportunityRequest" />.</returns>
	protected FindJobOpportunityRequest GenerateFindJobOpportunityRequest() => new(
		JobOpportunityId.New(),
		true);

	#endregion

	#region Core - Entities - Job Opportunity

	/// <summary>
	///   Generates a valid <see cref="JobOpportunity" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunity" />.</returns>
	protected JobOpportunity GenerateJobOpportunity() => JobOpportunity.Create(
		GenerateJobOpportunityName(),
		GenerateJobOpportunityDescription(),
		GenerateJobOpportunityLocation(),
		GenerateJobOpportunityContract(),
		GenerateJobOpportunityRequirements(5)
	);

	/// <summary>
	///   Generates a valid name for the <see cref="JobOpportunityName" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityName" />.</returns>
	protected JobOpportunityName GenerateJobOpportunityName() => new(Faker.Name.JobTitle().ClampLength(3, 32));

	/// <summary>
	///   Generates a valid description for the <see cref="JobOpportunityDescription" /> class.
	/// </summary>
	/// <returns>I will return a valid <see cref="JobOpportunityDescription" />.</returns>
	protected JobOpportunityDescription GenerateJobOpportunityDescription() => new(Faker.Lorem.Paragraph().ClampLength(10, 500));

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityLocation" />.
	/// </summary>
	/// <returns>If will return a valid <see cref="JobOpportunityLocation" />.</returns>
	protected JobOpportunityLocation GenerateJobOpportunityLocation() => new(
		Faker.PickRandom<LocationType>(),
		Faker.Address.City().ClampLength(3, 32),
		Faker.Address.StateAbbr().ClampLength(2, 2)
	);

	/// <summary>
	///   Generates a valid <see cref="JobOpportunityContract" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunityContract" />.</returns>
	protected JobOpportunityContract GenerateJobOpportunityContract() => new(
		Faker.PickRandom<ContractType>(),
		Faker.Random.Decimal(1000, 10000),
		Faker.Random.Decimal(10000, 100000)
	);

	/// <summary>
	///   Generates a list of requirements.
	/// </summary>
	/// <param name="count">The count of requirements to generate.</param>
	/// <returns>Returns a list of requirements.</returns>
	protected JobOpportunityRequirements GenerateJobOpportunityRequirements(int count)
	{
		var values = Enumerable
			.Range(0, count)
			.Select(_ => Faker.Lorem.Word().ClampLength(3, 32))
			.ToList();

		return new JobOpportunityRequirements(values);
	}

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
		GenerateCandidateEducation(2).ToList(),
		GenerateCandidateExperience(3).ToList(),
		jobOpportunities,
		GenerateCandidateSocialNetwork(),
		GenerateCandidateLanguage(2).ToList());

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="Candidate" />.</returns>
	private CandidateName GenerateCandidateName() => new(
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.FirstName().ClampLength(3, 32),
		Faker.Name.LastName().ClampLength(3, 32));

	/// <summary>
	///   Generates a valid <see cref="CandidateEmail" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateEmail" />.</returns>
	private CandidateEmail GenerateCandidateEmail() => new(
		Faker.Internet.Email());

	/// <summary>
	///   Generates a valid <see cref="CandidateDocument" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateDocument" />.</returns>
	private CandidateDocument GenerateCandidateDocument() => new(
		Faker.Person.Cpf());

	/// <summary>
	///   Generates a valid <see cref="CandidateDateOfBirth" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateDateOfBirth" />.</returns>
	private CandidateDateOfBirth GenerateCandidateDateOfBirth() => new(
		DateOnly.FromDateTime(Faker.Date.Past(16, DateTime.Now.AddYears(-60))));

	/// <summary>
	///   Generates a valid <see cref="CandidateSeniority" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateSeniority" />.</returns>
	private CandidateSeniority GenerateCandidateSeniority() => new(
		Faker.PickRandom<ExperienceLevel>());

	/// <summary>
	///   Generates a valid <see cref="CandidateDisability" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateDisability" />.</returns>
	private CandidateDisability GenerateCandidateDisability() => new(
		Faker.Random.Bool(),
		Faker.Random.Bool(),
		Faker.Random.Bool(),
		Faker.Random.Bool());

	/// <summary>
	///   Generates a valid <see cref="CandidateGender" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateGender" />.</returns>
	private CandidateGender GenerateCandidateGender() => new(
		Faker.PickRandom<Gender>());

	/// <summary>
	///   Generates a valid <see cref="CandidatePhoneNumber" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidatePhoneNumber" />.</returns>
	private CandidatePhoneNumber GenerateCandidatePhoneNumber() => new(
		Faker.Random.Number(10, 99).ToString(CultureInfo.InvariantCulture),
		Faker.Random.Number(900000000, 999999999).ToString(CultureInfo.InvariantCulture));

	/// <summary>
	///   Generates a valid <see cref="CandidateAddress" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateAddress" />.</returns>
	private CandidateAddress GenerateCandidateAddress() => new(
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
	private IEnumerable<CandidateEducation> GenerateCandidateEducation(int count) => Enumerable
		.Range(1, count)
		.Select(_ => new CandidateEducation(
			DateOnly.FromDateTime(Faker.Date.Past()),
			DateOnly.FromDateTime(Faker.Date.Past()),
			Faker.Company.CompanyName(),
			Faker.Lorem.Paragraph().ClampLength(10, 500),
			Faker.PickRandom<Degree>()))
		.AsEnumerable();


	/// <summary>
	///   Generates a valid <see cref="CandidateExperience" />.
	/// </summary>
	/// <param name="count">The count of languages to generate.</param>
	/// <returns>Returns a valid <see cref="CandidateExperience" />.</returns>
	private IEnumerable<CandidateExperience> GenerateCandidateExperience(int count) => Enumerable
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
	private IEnumerable<CandidateLanguage> GenerateCandidateLanguage(int count) =>
		Enumerable.Range(1, count)
			.Select(_ => new CandidateLanguage(
				Faker.Lorem.Word(),
				Faker.PickRandom<LanguageProficiency>()))
			.AsEnumerable();


	/// <summary>
	///   Generates a valid <see cref="CandidateSocialNetwork" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CandidateSocialNetwork" />.</returns>
	private CandidateSocialNetwork GenerateCandidateSocialNetwork() => new(
		Faker.Internet.Url(),
		Faker.Internet.Url());

	#endregion

	#region UseCases - Create

	/// <summary>
	///   Generates a valid request for the <see cref="CreateJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="CreateJobOpportunityRequest" />.</returns>
	protected CreateJobOpportunityRequest GenerateCreateJobOpportunityRequest() => new(GenerateCreateJobOpportunityInput());

	/// <summary>
	///   Generates a valid input for the <see cref="CreateJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="CreateJobOpportunityInput" />.</returns>
	private CreateJobOpportunityInput GenerateCreateJobOpportunityInput() => new(
		GenerateJobOpportunityName(),
		GenerateJobOpportunityDescription(),
		GenerateJobOpportunityLocation(),
		GenerateJobOpportunityContract(),
		GenerateJobOpportunityRequirements(5));

	#endregion

	#region UseCases - List

	/// <summary>
	///   Generates a valid paged list of job opportunities.
	/// </summary>
	/// <param name="pageSize">The page size.</param>
	/// <param name="pageNumber">The current page number.</param>
	/// <param name="totalCount">The total number of items.</param>
	/// <returns>It will return a paged list of job opportunities.</returns>
	protected PagedList<JobOpportunity> GeneratePagedListOfJobOpportunities(int pageSize, int pageNumber, int totalCount) => new(
		GenerateListOfJobOpportunities(totalCount),
		totalCount,
		pageNumber,
		pageSize);

	/// <summary>
	///   Generates a valid list of job opportunities.
	/// </summary>
	/// <param name="max">The maximum number of job opportunities to generate.</param>
	/// <returns>It will return a list of job opportunities.</returns>
	private IEnumerable<JobOpportunity> GenerateListOfJobOpportunities(int max) => Enumerable
		.Range(1, max)
		.Select(_ => GenerateJobOpportunity())
		.ToList()
		.AsEnumerable();

	#endregion

	#region UseCases - Update

	/// <summary>
	///   Generates a valid request for the <see cref="UpdateJobOpportunityUseCase" /> class.
	/// </summary>
	/// <param name="trackChanges">Should EF keep track of the changes?</param>
	/// <returns>It will return a valid <see cref="UpdateJobOpportunityRequest" />.</returns>
	protected UpdateJobOpportunityRequest GenerateUpdateJobOpportunityRequest(bool trackChanges) => new(
		JobOpportunityId.New(),
		GenerateValidInput(),
		trackChanges);

	/// <summary>
	///   Generates a valid input for the <see cref="UpdateJobOpportunityRequest" /> record.
	/// </summary>
	/// <returns>It will return a valid <see cref="UpdateJobOpportunityInput" />.</returns>
	private UpdateJobOpportunityInput GenerateValidInput() => new(
		GenerateJobOpportunityName(),
		GenerateJobOpportunityDescription(),
		GenerateJobOpportunityLocation(),
		GenerateJobOpportunityContract(),
		GenerateJobOpportunityRequirements(5));

	#endregion
}