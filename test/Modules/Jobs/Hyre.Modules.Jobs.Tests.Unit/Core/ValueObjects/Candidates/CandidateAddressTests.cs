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
///   Unit tests for the <see cref="CandidateAddress" /> class.
/// </summary>
public sealed class CandidateAddressTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		const string validCountry = "Brazil";
		const string validState = "Sao Paulo";
		const string validCity = "Sao Paulo";
		const string validNeighborhood = "Vila Mariana";
		const string validComplement = "Apartment 101";
		const int validNumber = 123;
		const string validZipCode = "01234567";

		// Act
		var sut = new CandidateAddress(validCountry, validState, validCity, validNeighborhood, validComplement, validNumber, validZipCode);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Country.Should().Be(validCountry);
		_ = sut.State.Should().Be(validState);
		_ = sut.City.Should().Be(validCity);
		_ = sut.Neighborhood.Should().Be(validNeighborhood);
		_ = sut.Complement.Should().Be(validComplement);
		_ = sut.Number.Should().Be(validNumber);
		_ = sut.ZipCode.Should().Be(validZipCode);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidCountry_ShouldThrowAnException))]
	[InlineData("", "Sao Paulo", "Sao Paulo", "Vila Mariana", null, 123, "01234567")]
	[InlineData("B", "Sao Paulo", "Sao Paulo", "Vila Mariana", null, 123, "01234567")]
	[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ", "Sao Paulo", "Sao Paulo", "Vila Mariana", null, 123,
		"01234567")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidCountry_ShouldThrowAnException(string country, string state, string city, string neighborhood,
		string? complement, int number, string zipCode)
	{
		// Act
		var act = () => new CandidateAddress(country, state, city, neighborhood, complement, number, zipCode);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressCountryNotValidException>()
			.WithMessage(CandidateErrorMessages.AddressCountryNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidState_ShouldThrowAnException))]
	[InlineData("Brazil", "", "Sao Paulo", "Vila Mariana", null, 123, "01234567")]
	[InlineData("Brazil", "S", "Sao Paulo", "Vila Mariana", null, 123, "01234567")]
	[InlineData("Brazil", "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ", "Sao Paulo", "Vila Mariana", null, 123,
		"01234567")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidState_ShouldThrowAnException(string country, string state, string city, string neighborhood,
		string? complement, int number, string zipCode)
	{
		// Act
		var act = () => new CandidateAddress(country, state, city, neighborhood, complement, number, zipCode);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressStateNotValidException>()
			.WithMessage(CandidateErrorMessages.AddressStateNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidCity_ShouldThrowAnException))]
	[InlineData("Brazil", "Sao Paulo", "", "Vila Mariana", null, 123, "01234567")]
	[InlineData("Brazil", "Sao Paulo", "S", "Vila Mariana", null, 123, "01234567")]
	[InlineData("Brazil", "Sao Paulo", "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ", "Vila Mariana", null, 123,
		"01234567")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidCity_ShouldThrowAnException(string country, string state, string city, string neighborhood,
		string? complement, int number, string zipCode)
	{
		// Act
		var act = () => new CandidateAddress(country, state, city, neighborhood, complement, number, zipCode);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressCityNotValidException>()
			.WithMessage(CandidateErrorMessages.AddressCityNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidComplement_ShouldThrowAnException))]
	[InlineData("Brazil", "Sao Paulo", "Sao Paulo", "Vila Mariana",
		"ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ",
		123, "01234567")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidComplement_ShouldThrowAnException(string country, string state, string city, string neighborhood,
		string? complement, int number, string zipCode)
	{
		// Act
		var act = () => new CandidateAddress(country, state, city, neighborhood, complement, number, zipCode);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressComplementNotValidException>()
			.WithMessage(CandidateErrorMessages.AddressComplementNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidZipCode_ShouldThrowAnException))]
	[InlineData("1234567")]
	[InlineData("123456789")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidZipCode_ShouldThrowAnException(string invalidZipCode)
	{
		// Act
		var act = () => new CandidateAddress("Brazil", "Sao Paulo", "Sao Paulo", "Vila Mariana", null, 123, invalidZipCode);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressZipCodeNotValidException>()
			.WithMessage(CandidateErrorMessages.ZipCodeNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidNeighborhood_ShouldThrowAnException))]
	[InlineData("N")]
	[InlineData(
		"Neighborhood with a very long name that exceeds the limit of characters allowed for neighborhood in the address Neighborhood with a very long name that exceeds the limit of characters allowed for neighborhood in the address")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidNeighborhood_ShouldThrowAnException(string invalidNeighborhood)
	{
		// Act
		var act = () => new CandidateAddress("Brazil", "Sao Paulo", "Sao Paulo", invalidNeighborhood, null, 123, "01234567");

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressNeighborhoodNotValidException>()
			.WithMessage(CandidateErrorMessages.AddressNeighborhoodNotValid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidNumber_ShouldThrowAnException))]
	[InlineData(0)]
	[InlineData(-1)]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidNumber_ShouldThrowAnException(int number)
	{
		// Arrange
		const string validCountry = "Brazil";
		const string validState = "Sao Paulo";
		const string validCity = "Sao Paulo";
		const string validNeighborhood = "Vila Mariana";
		const string validComplement = "Apartment 101";
		const string validZipCode = "01234567";

		// Act
		var act = () => new CandidateAddress(validCountry, validState, validCity, validNeighborhood, validComplement, number, validZipCode);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateAddressNumberNotValidException>()
			.WithMessage(CandidateErrorMessages.AddressNumberNotValid);
	}
}