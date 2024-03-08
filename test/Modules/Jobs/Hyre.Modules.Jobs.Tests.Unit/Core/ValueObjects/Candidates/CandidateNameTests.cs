// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

public sealed class CandidateNameTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var generated = GenerateValidName();

		// Act
		var sut = new CandidateName(
			generated.FirstName,
			generated.MiddleName,
			generated.LastName);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.FirstName.Should().NotBeEmpty();
		_ = sut.MiddleName.Should().NotBeEmpty();
		_ = sut.LastName.Should().NotBeEmpty();
		_ = sut.FirstName.Should().Be(generated.FirstName);
		_ = sut.MiddleName.Should().Be(generated.MiddleName);
		_ = sut.LastName.Should().Be(generated.LastName);
	}

	[Fact(DisplayName = nameof(Constructor_WithFirstNameTooShort_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithFirstNameTooShort_ShouldThrowAnException()
	{
		// Arrange
		var firstName = Faker.Name.FirstName().ClampLength(0, 2);
		var lastName = Faker.Name.LastName().ClampLength(3, 32);

		// Act
		var act = () => new CandidateName(firstName, null, lastName);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateFirstNameTooShortException>()
			.WithMessage(CandidateErrorMessages.FirstNameTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithFirstNameTooLong_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithFirstNameTooLong_ShouldThrowAnException()
	{
		// Arrange
		var firstName = Faker.Name.FirstName().ClampLength(33, 100);
		var lastName = Faker.Name.LastName().ClampLength(3, 32);

		// Act
		var act = () => new CandidateName(firstName, null, lastName);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateFirstNameTooLongException>()
			.WithMessage(CandidateErrorMessages.FirstNameTooLong);
	}

	[Fact(DisplayName = nameof(Constructor_WithMiddleNameTooShort_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithMiddleNameTooShort_ShouldThrowAnException()
	{
		// Arrange
		var firstName = Faker.Name.FirstName().ClampLength(3, 32);
		var middleName = Faker.Name.FirstName().ClampLength(0, 2);
		var lastName = Faker.Name.LastName().ClampLength(3, 32);

		// Act
		var act = () => new CandidateName(firstName, middleName, lastName);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateMiddleNameTooShortException>()
			.WithMessage(CandidateErrorMessages.MiddleNameTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithMiddleNameTooLong_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithMiddleNameTooLong_ShouldThrowAnException()
	{
		// Arrange
		var firstName = Faker.Name.FirstName().ClampLength(3, 32);
		var middleName = Faker.Name.FirstName().ClampLength(33, 100);
		var lastName = Faker.Name.LastName().ClampLength(3, 32);

		// Act
		var act = () => new CandidateName(firstName, middleName, lastName);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateMiddleNameTooLongException>()
			.WithMessage(CandidateErrorMessages.MiddleNameTooLong);
	}

	[Fact(DisplayName = nameof(Constructor_WithLastNameTooShort_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithLastNameTooShort_ShouldThrowAnException()
	{
		// Arrange
		var firstName = Faker.Name.FirstName().ClampLength(3, 32);
		var lastName = Faker.Name.LastName().ClampLength(0, 2);

		// Act
		var act = () => new CandidateName(firstName, null, lastName);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateLastNameTooShortException>()
			.WithMessage(CandidateErrorMessages.LastNameTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithLastNameTooLong_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithLastNameTooLong_ShouldThrowAnException()
	{
		// Arrange
		var firstName = Faker.Name.FirstName().ClampLength(3, 32);
		var lastName = Faker.Name.LastName().ClampLength(33, 100);

		// Act
		var act = () => new CandidateName(firstName, null, lastName);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateLastNameTooLongException>()
			.WithMessage(CandidateErrorMessages.LastNameTooLong);
	}
}