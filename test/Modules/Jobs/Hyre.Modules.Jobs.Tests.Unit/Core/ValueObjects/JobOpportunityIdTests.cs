// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Shared.Abstractions.Exceptions;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects;

/// <summary>
///   Contains tests for the <see cref="JobOpportunityId" /> value object.
/// </summary>
public sealed class JobOpportunityIdTests
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange 
		var value = Guid.NewGuid();

		// Act
		var id = new JobOpportunityId(value);

		// Assert
		_ = id.Should().NotBeNull();
		_ = id.Value.Should().NotBeEmpty();
	}

	[Fact(DisplayName = nameof(Constructor_WithEmptyValue_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithEmptyValue_ShouldThrowAnException()
	{
		// Arrange
		var value = Guid.Empty;

		// Act
		var act = () => new JobOpportunityId(value);

		// Assert
		_ = act.Should().ThrowExactly<DomainException>();
	}

	[Fact(DisplayName = nameof(New_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void New_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var id = JobOpportunityId.New();

		// Assert
		_ = id.Should().NotBeNull();
		_ = id.Value.Should().NotBeEmpty();
	}
}