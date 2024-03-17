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
///   Unit tests for the <see cref="CandidateDateOfBirth" /> class.
/// </summary>
public sealed class CandidateDateOfBirthTests
{
	[Fact(DisplayName = nameof(Constructor_WithValidDateOfBirth_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidDateOfBirth_ShouldCreateAnInstance()
	{
		// Arrange
		var validDateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

		// Act
		var sut = new CandidateDateOfBirth(validDateOfBirth);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().Be(validDateOfBirth);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidDateOfBirth_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidDateOfBirth_ShouldThrowAnException()
	{
		// Arrange
		var invalidDateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));

		// Act
		var act = () => new CandidateDateOfBirth(invalidDateOfBirth);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateDateOfBirthNotValidException>()
			.WithMessage(CandidateErrorMessages.DateOfBirthNotValid);
	}

	[Fact(DisplayName = nameof(ImplicitOperator_WithValidCandidateDateOfBirth_ShouldReturnDateOnly))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperator_WithValidCandidateDateOfBirth_ShouldReturnDateOnly()
	{
		// Arrange
		var validDateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));
		var candidateDateOfBirth = new CandidateDateOfBirth(validDateOfBirth);

		// Act
		DateOnly result = candidateDateOfBirth;

		// Assert
		_ = result.Should().Be(validDateOfBirth);
	}

	[Fact(DisplayName = nameof(ImplicitOperator_WithValidDateOnly_ShouldReturnCandidateDateOfBirth))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperator_WithValidDateOnly_ShouldReturnCandidateDateOfBirth()
	{
		// Arrange
		var validDateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

		// Act
		CandidateDateOfBirth result = validDateOfBirth;

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Value.Should().Be(validDateOfBirth);
	}
}