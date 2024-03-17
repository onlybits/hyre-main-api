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
///   Unit tests for the <see cref="CandidateExperience" /> class.
/// </summary>
public sealed class CandidateExperienceTests
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string position = "Software Engineer";
		const string company = "Tech Company";
		const string description = "Developed software applications";

		// Act
		var sut = new CandidateExperience(startDate, endDate, position, company, description);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.StartDate.Should().Be(startDate);
		_ = sut.EndDate.Should().Be(endDate);
		_ = sut.Position.Should().Be(position);
		_ = sut.Company.Should().Be(company);
		_ = sut.Description.Should().Be(description);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidStartDate_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidStartDate_ShouldThrowAnException()
	{
		// Arrange
		var invalidStartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string position = "Software Engineer";
		const string company = "Tech Company";
		const string description = "Developed software applications";

		// Act
		var act = () => new CandidateExperience(invalidStartDate, endDate, position, company, description);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateExperienceStartDateInvalidException>()
			.WithMessage(CandidateErrorMessages.ExperienceStartDateInvalid);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidEndDate_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidEndDate_ShouldThrowAnException()
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var invalidEndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));
		const string position = "Software Engineer";
		const string company = "Tech Company";
		const string description = "Developed software applications";

		// Act
		var act = () => new CandidateExperience(startDate, invalidEndDate, position, company, description);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateExperienceEndDateInvalidException>()
			.WithMessage(CandidateErrorMessages.ExperienceEndDateInvalid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidPosition_ShouldThrowAnException))]
	[InlineData("P")]
	[InlineData(
		"Position with a very long name that exceeds the limit of characters allowed for position in the experience")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidPosition_ShouldThrowAnException(string invalidPosition)
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string company = "Tech Company";
		const string description = "Developed software applications";

		// Act
		var act = () => new CandidateExperience(startDate, endDate, invalidPosition, company, description);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateExperiencePositionInvalidException>()
			.WithMessage(CandidateErrorMessages.ExperiencePositionInvalid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidCompany_ShouldThrowAnException))]
	[InlineData("C")]
	[InlineData(
		"Company with a very long name that exceeds the limit of characters allowed for company in the experience")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidCompany_ShouldThrowAnException(string invalidCompany)
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string position = "Software Engineer";
		const string description = "Developed software applications";

		// Act
		var act = () => new CandidateExperience(startDate, endDate, position, invalidCompany, description);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateExperienceCompanyInvalidException>()
			.WithMessage(CandidateErrorMessages.ExperienceCompanyInvalid);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidDescription_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidDescription_ShouldThrowAnException()
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string position = "Software Engineer";
		const string company = "Tech Company";
		const string invalidDescription = "D";

		// Act
		var act = () => new CandidateExperience(startDate, endDate, position, company, invalidDescription);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateExperienceDescriptionInvalidException>()
			.WithMessage(CandidateErrorMessages.ExperienceDescriptionInvalid);
	}
}