// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.JobOpportunities;

/// <summary>
///   Unit tests for the <see cref="JobOpportunityDescription" /> value object.
/// </summary>
public sealed class JobOpportunityDescriptionTests : BaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var value = Faker.Lorem.Paragraph().ClampLength(10, 500);

		// Act
		var sut = new JobOpportunityDescription(value);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
		_ = sut.Value.Should().Be(value);
	}

	[Fact(DisplayName = nameof(Constructor_WithValueTooShort_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValueTooShort_ShouldThrowADomainException()
	{
		// Arrange
		var value = Faker.Lorem.Paragraph().ClampLength(0, 9);

		// Act
		var act = () => new JobOpportunityDescription(value);

		// Assert
		_ = act.Should().ThrowExactly<JobOpportunityDescriptionTooShortException>()
			.WithMessage(JobOpportunityErrorMessages.DescriptionTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithValueTooLong_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValueTooLong_ShouldThrowADomainException()
	{
		// Arrange
		var value = Faker.Lorem.Paragraph().ClampLength(501, 1000);

		// Act
		var act = () => new JobOpportunityDescription(value);

		// Assert
		_ = act.Should().ThrowExactly<JobOpportunityDescriptionTooLongException>()
			.WithMessage(JobOpportunityErrorMessages.DescriptionTooLong);
	}
}