// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

/// <summary>
///   Unit tests for the <see cref="CandidateEmail" /> class.
/// </summary>
public sealed class CandidateEmailTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var generated = Faker.Internet.Email();

		// Act
		var sut = new CandidateEmail(generated);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
		_ = sut.Value.Should().Be(generated);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidEmail_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidEmail_ShouldThrowAnException()
	{
		// Arrange
		const string invalidEmail = "invalid-email";

		// Act
		var act = () => new CandidateEmail(invalidEmail);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateEmailInvalidException>()
			.WithMessage(CandidateErrorMessages.EmailInvalid(invalidEmail));
	}

	[Fact(DisplayName = nameof(ImplicitOperator_WithValidCandidateEmail_ShouldReturnString))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperator_WithValidCandidateEmail_ShouldReturnString()
	{
		// Arrange
		var generated = Faker.Internet.Email();
		var sut = new CandidateEmail(generated);

		// Act
		string result = sut;

		// Assert
		_ = result.Should().NotBeEmpty();
		_ = result.Should().Be(generated);
	}

	[Fact(DisplayName = nameof(ImplicitOperator_WithValidString_ShouldReturnCandidateEmail))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void ImplicitOperator_WithValidString_ShouldReturnCandidateEmail()
	{
		// Arrange
		var generated = Faker.Internet.Email();

		// Act
		CandidateEmail result = generated;

		// Assert
		_ = result.Should().NotBeNull();
		_ = result.Value.Should().NotBeEmpty();
		_ = result.Value.Should().Be(generated);
	}
}