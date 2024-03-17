// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Globalization;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
using Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Infrastructure;

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
	///   This method will seed the database with the given <see cref="JobOpportunity" /> and a list of
	///   <see cref="Candidate" />.
	/// </summary>
	/// <param name="context">The jobs repository context.</param>
	/// <param name="trackChanges">Should the changes be tracked?</param>
	/// <param name="jobOpportunity">The <see cref="JobOpportunity" /> to seed the database with.</param>
	/// <param name="candidates">The <see cref="Candidate" /> to seed the database with.</param>
	internal async Task SeedDatabaseAsync(
		JobsRepositoryContext context,
		bool trackChanges = false,
		JobOpportunity? jobOpportunity = null,
		IEnumerable<Candidate>? candidates = null)
	{
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
		if (!trackChanges)
		{
			context.ChangeTracker.Clear();
		}
	}

	#region UseCases

	/// <summary>
	///   Generates a valid <see cref="CreateCandidateInput" />.
	/// </summary>
	/// <returns>Returns a valid <see cref="CreateCandidateInput" />.</returns>
	protected CreateCandidateInput GenerateCreateCandidateInput(CandidateEmail? email = null) => new(
		GenerateCandidateName(),
		email ?? GenerateCandidateEmail(),
		GenerateCandidateDocument(),
		GenerateCandidateDateOfBirth(),
		GenerateCandidateSeniority(),
		GenerateCandidateDisability(),
		GenerateCandidateGender(),
		GenerateCandidatePhoneNumber(),
		GenerateCandidateAddress(),
		GenerateCandidateEducation(2).ToList(),
		GenerateCandidateExperience(3).ToList(),
		GenerateCandidateSocialNetwork(),
		GenerateCandidateLanguage(2).ToList());

	#endregion

	#region Core Entity - Candidate

	/// <summary>
	///   Generates a list of <see cref="Candidate" />.
	/// </summary>
	/// <param name="count">The count of candidates to generate.</param>
	/// <param name="jobOpportunities">The job opportunities to associate with the candidates.</param>
	/// <returns>Returns a list of <see cref="Candidate" />.</returns>
	protected IEnumerable<Candidate> GenerateCandidates(int count, ICollection<JobOpportunity> jobOpportunities) => Enumerable.Range(1, count)
		.Select(_ => GenerateCandidate(jobOpportunities))
		.AsEnumerable();

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
	protected CandidateName GenerateCandidateName() => new(
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
	private IEnumerable<CandidateEducation> GenerateCandidateEducation(int count)
	{
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));
		return Enumerable
			.Range(1, count)
			.Select(x => new CandidateEducation(
				Faker.Date.BetweenDateOnly(startDate, endDate),
				DateOnly.FromDateTime(DateTime.Now),
				Faker.Company.CompanyName(),
				Faker.Lorem.Paragraph().ClampLength(3, 50),
				Faker.PickRandom<Degree>()))
			.AsEnumerable();
	}


	/// <summary>
	///   Generates a valid <see cref="CandidateExperience" />.
	/// </summary>
	/// <param name="count">The count of languages to generate.</param>
	/// <returns>Returns a valid <see cref="CandidateExperience" />.</returns>
	private IEnumerable<CandidateExperience> GenerateCandidateExperience(int count)
	{
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));
		return Enumerable
			.Range(1, count)
			.Select(_ => new CandidateExperience(
				Faker.Date.BetweenDateOnly(startDate, endDate),
				DateOnly.FromDateTime(DateTime.Now),
				Faker.Name.JobTitle(),
				Faker.Company.CompanyName(),
				Faker.Name.JobDescriptor()))
			.AsEnumerable();
	}

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