// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.Entities;

/// <summary>
///   Unit tests for the <see cref="JobOpportunity" /> class.
/// </summary>
public sealed class JobOpportunityTests : JobOpportunityTestsFixture
{
	[Fact(DisplayName = nameof(Create_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void Create_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var name = GenerateValidName();
		var description = GenerateValidDescription();
		var location = GenerateValidLocation();
		var contract = GenerateValidContract();

		// Act
		var sut = JobOpportunity.Create(name, description, location, contract);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
		_ = sut.Name.Should().Be(name);
		_ = sut.Description.Should().Be(description);
		_ = sut.CreatedAt.Should().NotBe(default!);
		_ = sut.CreatedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
		_ = sut.Location.Should().NotBeNull();
	}

	[Fact(DisplayName = nameof(UpdateName_WithValidParameters_ShouldUpdateName))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateName_WithValidParameters_ShouldUpdateName()
	{
		// Arrange
		var name = GenerateValidName();
		var description = GenerateValidDescription();
		var location = GenerateValidLocation();
		var contract = GenerateValidContract();

		var newName = GenerateValidName();

		// Act
		var sut = JobOpportunity.Create(name, description, location, contract);
		sut.UpdateName(newName);

		// Assert
		_ = sut.Name.Should().Be(newName);
	}

	[Fact(DisplayName = nameof(UpdateDescription_WithValidParameters_ShouldUpdateDescription))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateDescription_WithValidParameters_ShouldUpdateDescription()
	{
		// Arrange
		var name = GenerateValidName();
		var description = GenerateValidDescription();
		var location = GenerateValidLocation();
		var contract = GenerateValidContract();

		var newDescription = GenerateValidDescription();

		// Act
		var sut = JobOpportunity.Create(name, description, location, contract);
		sut.UpdateDescription(newDescription);

		// Assert
		_ = sut.Description.Should().Be(newDescription);
	}

	[Fact(DisplayName = nameof(UpdateLocation_WithValidParameters_ShouldUpdateLocation))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateLocation_WithValidParameters_ShouldUpdateLocation()
	{
		// Arrange
		var name = GenerateValidName();
		var description = GenerateValidDescription();
		var location = GenerateValidLocation();
		var contract = GenerateValidContract();

		var newLocation = GenerateValidLocation();

		// Act
		var sut = JobOpportunity.Create(name, description, location, contract);
		sut.UpdateLocation(newLocation);

		// Assert
		_ = sut.Location.Should().Be(newLocation);
	}

	[Fact(DisplayName = nameof(UpdateContract_WithValidParameters_ShouldUpdateContract))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateContract_WithValidParameters_ShouldUpdateContract()
	{
		// Arrange
		var name = GenerateValidName();
		var description = GenerateValidDescription();
		var location = GenerateValidLocation();
		var contract = GenerateValidContract();

		var newContract = GenerateValidContract();

		// Act
		var sut = JobOpportunity.Create(name, description, location, contract);
		sut.UpdateContract(newContract);

		// Assert
		_ = sut.Contract.Should().Be(newContract);
	}
}