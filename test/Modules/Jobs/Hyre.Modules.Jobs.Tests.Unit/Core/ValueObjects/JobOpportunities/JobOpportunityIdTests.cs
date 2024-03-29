﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Unit tests for the <see cref="JobOpportunityId" /> value object.
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
		var sut = new JobOpportunityId(value);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
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
		_ = act.Should().ThrowExactly<JobOpportunityIdHasEmptyValueException>()
			.WithMessage(JobOpportunityErrorMessages.IdCannotBeEmpty);
	}

	[Fact(DisplayName = nameof(New_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void New_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var sut = JobOpportunityId.New();

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
	}

	[Fact(DisplayName = nameof(ImplicitOperatorToValueObject_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperatorToValueObject_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var value = Guid.NewGuid();

		// Act
		JobOpportunityId result = value;

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Value.Should().NotBeEmpty();
		_ = result.Value.Should().Be(value);
	}

	[Fact(DisplayName = nameof(ImplicitOperatorToGuid_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperatorToGuid_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var value = Guid.NewGuid();
		var sut = new JobOpportunityId(value);

		// Act
		Guid result = sut;

		// Assert
		_ = result.Should().NotBeEmpty();
		_ = result.Should().Be(value);
	}
}