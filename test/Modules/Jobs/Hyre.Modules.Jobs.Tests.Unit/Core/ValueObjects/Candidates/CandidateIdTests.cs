// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

/// <summary>
///   Unit tests for the <see cref="CandidateId" /> value object.
/// </summary>
public sealed class CandidateIdTests
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange 
		var value = Guid.NewGuid();

		// Act
		var sut = new CandidateId(value);

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
		var act = () => new CandidateId(value);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateIdCannotBeEmptyException>()
			.WithMessage(CandidateErrorMessages.IdCannotBeEmpty);
	}

	[Fact(DisplayName = nameof(New_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void New_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var sut = CandidateId.New();

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
	}

	[Fact(DisplayName = nameof(ImplicitOperatorToGuid_WhenCalled_ShouldReturnGuid))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperatorToGuid_WhenCalled_ShouldReturnGuid()
	{
		// Arrange
		var value = Guid.NewGuid();
		var sut = new CandidateId(value);

		// Act
		Guid result = sut;

		// Assert
		_ = result.Should().Be(value);
	}

	[Fact(DisplayName = nameof(ImplicitOperatorToValueObject_WhenCalled_ShouldReturnValueObject))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperatorToValueObject_WhenCalled_ShouldReturnValueObject()
	{
		// Arrange
		var value = Guid.NewGuid();
		var sut = new CandidateId(value);

		// Act
		CandidateId result = value;

		// Assert
		_ = result.Should().Be(sut);
	}
}