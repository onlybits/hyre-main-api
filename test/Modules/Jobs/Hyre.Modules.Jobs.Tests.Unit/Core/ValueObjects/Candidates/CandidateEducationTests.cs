// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

/// <summary>
///   Unit tests for the <see cref="CandidateEducation" /> class.
/// </summary>
public sealed class CandidateEducationTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string institution = "University of Example";
		const string course = "Computer Science";
		var degree = Faker.PickRandom<Degree>();

		// Act
		var sut = new CandidateEducation(startDate, endDate, institution, course, degree);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.StartDate.Should().Be(startDate);
		_ = sut.EndDate.Should().Be(endDate);
		_ = sut.Institution.Should().Be(institution);
		_ = sut.Course.Should().Be(course);
		_ = sut.Degree.Should().Be(degree);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidStartDate_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidStartDate_ShouldThrowAnException()
	{
		// Arrange
		var invalidStartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string institution = "University of Example";
		const string course = "Computer Science";
		var degree = Faker.PickRandom<Degree>();

		// Act
		var act = () => new CandidateEducation(invalidStartDate, endDate, institution, course, degree);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateEducationStartDateInvalidException>()
			.WithMessage(CandidateErrorMessages.EducationStartDateInvalid);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidEndDate_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidEndDate_ShouldThrowAnException()
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var invalidEndDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-10));
		const string institution = "University of Example";
		const string course = "Computer Science";
		var degree = Faker.PickRandom<Degree>();

		// Act
		var act = () => new CandidateEducation(startDate, invalidEndDate, institution, course, degree);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateEducationEndDateInvalidException>()
			.WithMessage(CandidateErrorMessages.EducationEndDateInvalid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidInstitution_ShouldThrowAnException))]
	[InlineData("A")]
	[InlineData(
		"Institution with a very long name that exceeds the limit of characters allowed for institution in the education")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidInstitution_ShouldThrowAnException(string invalidInstitution)
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string course = "Computer Science";
		var degree = Faker.PickRandom<Degree>();

		// Act
		var act = () => new CandidateEducation(startDate, endDate, invalidInstitution, course, degree);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateEducationInstitutionInvalidException>()
			.WithMessage(CandidateErrorMessages.EducationInstitutionInvalid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidCourse_ShouldThrowAnException))]
	[InlineData("A")]
	[InlineData("Course with a very long name that exceeds the limit of characters allowed for course in the education")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidCourse_ShouldThrowAnException(string invalidCourse)
	{
		// Arrange
		var startDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-5));
		var endDate = DateOnly.FromDateTime(DateTime.Now);
		const string institution = "University of Example";
		var degree = Faker.PickRandom<Degree>();

		// Act
		var act = () => new CandidateEducation(startDate, endDate, institution, invalidCourse, degree);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateEducationCourseInvalidException>()
			.WithMessage(CandidateErrorMessages.EducationCourseInvalid);
	}
}