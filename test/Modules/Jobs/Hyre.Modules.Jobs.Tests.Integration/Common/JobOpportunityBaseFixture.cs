// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Globalization;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
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
	///   Generates a list of <see cref="JobOpportunity" />.
	/// </summary>
	/// <param name="count">The number of <see cref="JobOpportunity" /> to generate.</param>
	/// <returns>It will return a list of <see cref="JobOpportunity" />.</returns>
	protected IReadOnlyList<JobOpportunity> GenerateJobOpportunities(int count) => Enumerable
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
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });

		jobOpportunity.AddCandidate(candidate);

		return jobOpportunity;
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
	protected JobOpportunityName GenerateValidName() => new(Faker.Name.JobTitle().ClampLength(3, 64));

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
	///   This method is responsible for seeding the database with the given job opportunities.
	/// </summary>
	/// <param name="jobOpportunities">The job opportunities to be seeded.</param>
	protected async Task SeedDatabase(IEnumerable<JobOpportunity> jobOpportunities)
	{
		var contextToSeed = CreateRepositoryContext();
		await contextToSeed.JobOpportunities.AddRangeAsync(jobOpportunities);
		_ = await contextToSeed.SaveChangesAsync();
	}


	#region Core Entity - Candidate

	/// <summary>
	///   Generates a valid <see cref="Candidate" />.
	/// </summary>
	/// <returns>The valid <see cref="Candidate" />.</returns>
	private Candidate GenerateCandidate(ICollection<JobOpportunity> jobOpportunities) => Candidate.Create(
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
	protected CandidateAddress GenerateCandidateAddress() => new(
		Faker.Address.Country().ClampLength(3, 50),
		Faker.Address.State().ClampLength(3, 50),
		Faker.Address.City().ClampLength(3, 50),
		Faker.Address.StreetName().ClampLength(3, 50),
		Faker.Address.Direction().ClampLength(3, 100),
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
}