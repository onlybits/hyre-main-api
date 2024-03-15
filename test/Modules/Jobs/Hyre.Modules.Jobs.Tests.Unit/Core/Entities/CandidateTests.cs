// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.Events;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.Entities;

/// <summary>
///   Unit tests for the <see cref="Candidate" /> class.
/// </summary>
public sealed class CandidateTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Create_WhenGivenValidParameters_ShouldCreateAnInstance))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void Create_WhenGivenValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var name = GenerateCandidateName();
		var email = GenerateCandidateEmail();
		var document = GenerateCandidateDocument();
		var dateOfBirth = GenerateCandidateDateOfBirth();
		var seniority = GenerateCandidateSeniority();
		var disability = GenerateCandidateDisability();
		var gender = GenerateCandidateGender();
		var phoneNumber = GenerateCandidatePhoneNumber();
		var address = GenerateCandidateAddress();
		var education = GenerateCandidateEducation(2);
		var experience = GenerateCandidateExperience(2).ToList();
		var jobOpportunities = new List<JobOpportunity>
		{
			GenerateJobOpportunity(),
			GenerateJobOpportunity()
		};
		var socialNetwork = GenerateCandidateSocialNetwork();
		var language = GenerateCandidateLanguage(2).ToList();

		// Act
		var sut = Candidate.Create(
			name,
			email,
			document,
			dateOfBirth,
			seniority,
			disability,
			gender,
			phoneNumber,
			address,
			education,
			experience,
			jobOpportunities,
			socialNetwork,
			language);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
		_ = sut.Name.Should().Be(name);
		_ = sut.Email.Should().Be(email);
		_ = sut.Document.Should().Be(document);
		_ = sut.DateOfBirth.Should().Be(dateOfBirth);
		_ = sut.Seniority.Should().Be(seniority);
		_ = sut.Disability.Should().Be(disability);
		_ = sut.Gender.Should().Be(gender);
		_ = sut.PhoneNumber.Should().Be(phoneNumber);
		_ = sut.Address.Should().Be(address);
		_ = sut.Educations.Should().BeEquivalentTo(education);
		_ = sut.Experiences.Should().BeEquivalentTo(experience);
		_ = sut.JobOpportunities.Should().BeEquivalentTo(jobOpportunities);
		_ = sut.SocialNetwork.Should().Be(socialNetwork);
		_ = sut.Languages.Should().BeEquivalentTo(language);
		_ = sut.CreatedAt.Should().NotBe(default!);
		_ = sut.CreatedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
		_ = sut.Events.Should().HaveCount(1);

		var @event = sut.Events.First() as CandidateCreatedEvent;
		_ = @event.Should().NotBeNull();
		_ = @event!.Email.Should().Be(email);
	}

	[Fact(DisplayName = nameof(UpdateName_WhenGivenValidName_ShouldUpdateName))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateName_WhenGivenValidName_ShouldUpdateName()
	{
		// Arrange
		var jobOpportunities = new List<JobOpportunity>
		{
			GenerateJobOpportunity()
		};
		var sut = GenerateCandidate(jobOpportunities);
		var newName = GenerateCandidateName();

		// Act
		sut.UpdateName(newName);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Name.Should().Be(newName);
	}
}