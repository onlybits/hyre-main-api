﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.Entities;

/// <summary>
///   Unit tests for the <see cref="JobOpportunity" /> class.
/// </summary>
public sealed class JobOpportunityTests : JobOpportunityBaseFixture
{
	[Fact(DisplayName = nameof(Create_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void Create_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var name = GenerateJobOpportunityName();
		var description = GenerateJobOpportunityDescription();
		var location = GenerateJobOpportunityLocation();
		var contract = GenerateJobOpportunityContract();
		var requirements = GenerateJobOpportunityRequirements(5);

		// Act
		var sut = JobOpportunity.Create(
			name,
			description,
			location,
			contract,
			requirements);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
		_ = sut.Name.Should().Be(name);
		_ = sut.Description.Should().Be(description);
		_ = sut.CreatedAt.Should().NotBe(default!);
		_ = sut.CreatedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
		_ = sut.Location.Should().NotBeNull();
		_ = sut.Location.Should().Be(location);
		_ = sut.Contract.Should().NotBeNull();
		_ = sut.Contract.Should().Be(contract);
		_ = sut.Requirements.Should().NotBeNull();
		_ = sut.Requirements.Should().BeEquivalentTo(requirements);
		_ = sut.Candidates.Should().BeNullOrEmpty();
	}

	[Fact(DisplayName = nameof(UpdateName_WithValidParameters_ShouldUpdateName))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateName_WithValidParameters_ShouldUpdateName()
	{
		// Arrange
		var name = GenerateJobOpportunityName();
		var description = GenerateJobOpportunityDescription();
		var location = GenerateJobOpportunityLocation();
		var contract = GenerateJobOpportunityContract();
		var requirements = GenerateJobOpportunityRequirements(5);

		var newName = GenerateJobOpportunityName();

		// Act
		var sut = JobOpportunity.Create(
			name,
			description,
			location,
			contract,
			requirements);

		sut.UpdateName(newName);

		// Assert
		_ = sut.Name.Should().Be(newName);
	}

	[Fact(DisplayName = nameof(UpdateDescription_WithValidParameters_ShouldUpdateDescription))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateDescription_WithValidParameters_ShouldUpdateDescription()
	{
		// Arrange
		var name = GenerateJobOpportunityName();
		var description = GenerateJobOpportunityDescription();
		var location = GenerateJobOpportunityLocation();
		var contract = GenerateJobOpportunityContract();
		var requirements = GenerateJobOpportunityRequirements(5);

		var newDescription = GenerateJobOpportunityDescription();

		// Act
		var sut = JobOpportunity.Create(
			name,
			description,
			location,
			contract,
			requirements);

		sut.UpdateDescription(newDescription);

		// Assert
		_ = sut.Description.Should().Be(newDescription);
	}

	[Fact(DisplayName = nameof(UpdateLocation_WithValidParameters_ShouldUpdateLocation))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateLocation_WithValidParameters_ShouldUpdateLocation()
	{
		// Arrange
		var name = GenerateJobOpportunityName();
		var description = GenerateJobOpportunityDescription();
		var location = GenerateJobOpportunityLocation();
		var contract = GenerateJobOpportunityContract();
		var requirements = GenerateJobOpportunityRequirements(5);

		var newLocation = GenerateJobOpportunityLocation();

		// Act
		var sut = JobOpportunity.Create(
			name,
			description,
			location,
			contract,
			requirements);

		sut.UpdateLocation(newLocation);

		// Assert
		_ = sut.Location.Should().Be(newLocation);
	}

	[Fact(DisplayName = nameof(UpdateContract_WithValidParameters_ShouldUpdateContract))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateContract_WithValidParameters_ShouldUpdateContract()
	{
		// Arrange
		var name = GenerateJobOpportunityName();
		var description = GenerateJobOpportunityDescription();
		var location = GenerateJobOpportunityLocation();
		var contract = GenerateJobOpportunityContract();
		var requirements = GenerateJobOpportunityRequirements(5);

		var newContract = GenerateJobOpportunityContract();

		// Act
		var sut = JobOpportunity.Create(
			name,
			description,
			location,
			contract,
			requirements);
		sut.UpdateContract(newContract);

		// Assert
		_ = sut.Contract.Should().Be(newContract);
	}

	[Fact(DisplayName = nameof(UpdateRequirements_WithValidParameters_ShouldUpdateRequirements))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateRequirements_WithValidParameters_ShouldUpdateRequirements()
	{
		// Arrange
		var name = GenerateJobOpportunityName();
		var description = GenerateJobOpportunityDescription();
		var location = GenerateJobOpportunityLocation();
		var contract = GenerateJobOpportunityContract();
		var requirements = GenerateJobOpportunityRequirements(5);

		var newRequirements = GenerateJobOpportunityRequirements(5);

		// Act
		var sut = JobOpportunity.Create(
			name,
			description,
			location,
			contract,
			requirements);
		sut.UpdateRequirements(newRequirements);

		// Assert
		_ = sut.Requirements.Should().BeEquivalentTo(newRequirements);
	}

	[Fact(DisplayName = nameof(AddCandidate_WhenPassingValidCandidate_ShouldAddCandidate))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void AddCandidate_WhenPassingValidCandidate_ShouldAddCandidate()
	{
		// Arrange
		var jobOpportunity = GenerateJobOpportunity();
		var sut = GenerateJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });

		// Act
		sut.AddCandidate(candidate);

		// Assert
		_ = sut.Candidates.Should().NotBeEmpty();
		_ = sut.Candidates.Should().Contain(candidate);
		_ = sut.Candidates.Should().HaveCount(1);
	}
}