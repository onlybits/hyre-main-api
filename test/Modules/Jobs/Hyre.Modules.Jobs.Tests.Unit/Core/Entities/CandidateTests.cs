// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
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
		var name = GenerateValidName();
		var email = GenerateCandidateEmail();
		var jobOpportunityId = JobOpportunityId.New();

		// Act
		var sut = Candidate.Create(jobOpportunityId, name, email);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
		_ = sut.Name.Should().Be(name);
		_ = sut.Email.Should().Be(email);
		_ = sut.CreatedAt.Should().NotBe(default!);
		_ = sut.CreatedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
		_ = sut.JobOpportunityId.Should().NotBe(default!);
		_ = sut.JobOpportunityId.Value.Should().NotBeEmpty();
		_ = sut.JobOpportunityId.Should().Be(jobOpportunityId);
		_ = sut.Events.Should().HaveCount(1);
	}

	[Fact(DisplayName = nameof(UpdateName_WhenGivenValidName_ShouldUpdateName))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateName_WhenGivenValidName_ShouldUpdateName()
	{
		// Arrange
		var sut = GenerateValidCandidate();
		var newName = GenerateValidName();

		// Act
		sut.UpdateName(newName);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Name.Should().Be(newName);
	}
}