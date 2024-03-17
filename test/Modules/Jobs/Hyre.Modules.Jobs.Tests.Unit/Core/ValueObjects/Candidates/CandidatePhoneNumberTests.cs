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
///   Unit tests for the <see cref="CandidatePhoneNumber" /> class.
/// </summary>
public sealed class CandidatePhoneNumberTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		const string validAreaCode = "11";
		const string validNumber = "123456789";

		// Act
		var sut = new CandidatePhoneNumber(validAreaCode, validNumber);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.AreaCode.Should().Be(validAreaCode);
		_ = sut.Number.Should().Be(validNumber);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidAreaCode_ShouldThrowAnException))]
	[InlineData("1", "123456789")]
	[InlineData("AB", "123456789")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidAreaCode_ShouldThrowAnException(string areaCode, string number)
	{
		// Act
		var act = () => new CandidatePhoneNumber(areaCode, number);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidatePhoneNumberAreaCodeNotValidException>()
			.WithMessage(CandidateErrorMessages.PhoneNumberAreaCodeNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidNumber_ShouldThrowAnException))]
	[InlineData("11", "12345678")]
	[InlineData("11", "ABCD56789")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidNumber_ShouldThrowAnException(string areaCode, string number)
	{
		// Act
		var act = () => new CandidatePhoneNumber(areaCode, number);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidatePhoneNumberValueNotValidException>()
			.WithMessage(CandidateErrorMessages.PhoneNumberValueNotValid);
	}
}