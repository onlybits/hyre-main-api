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

		// Act
		var sut = JobOpportunity.Create(name);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
	}

	[Fact(DisplayName = nameof(UpdateName_WithValidParameters_ShouldUpdateName))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void UpdateName_WithValidParameters_ShouldUpdateName()
	{
		// Arrange
		var name = GenerateValidName();
		var newName = GenerateValidName();
		var sut = JobOpportunity.Create(name);

		// Act
		sut.UpdateName(newName);

		// Assert
		_ = sut.Name.Should().Be(newName);
	}
}