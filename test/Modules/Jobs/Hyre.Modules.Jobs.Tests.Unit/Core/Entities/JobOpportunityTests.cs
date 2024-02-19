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
		// Act
		var sut = JobOpportunity.Create(name, description);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
		_ = sut.Name.Should().Be(name);
		_ = sut.Description.Should().Be(description);
		_ = sut.CreatedAt.Should().NotBe(default!);
		_ = sut.CreatedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
	}

	[Fact(DisplayName = nameof(UpdateName_WithValidParameters_ShouldUpdateName))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateName_WithValidParameters_ShouldUpdateName()
	{
		// Arrange
		var name = GenerateValidName();
		var description = GenerateValidDescription();

		var newName = GenerateValidName();
		var sut = JobOpportunity.Create(name, description);

		// Act
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

		var newDescription = GenerateValidDescription();
		var sut = JobOpportunity.Create(name, description);

		// Act
		sut.UpdateDescription(newDescription);

		// Assert
		_ = sut.Description.Should().Be(newDescription);
	}
}