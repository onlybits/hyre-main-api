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

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects;

/// <summary>
///   Unit tests for the <see cref="JobOpportunityRequirements" /> value object.
/// </summary>
public sealed class JobOpportunityRequirementsTests : BaseFixture
{
	[Theory(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	[InlineData(1)]
	[InlineData(3)]
	[InlineData(5)]
	[InlineData(10)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance(int count)
	{
		// Arrange
		var values = Enumerable.Range(0, count)
			.Select(_ => Faker.Lorem.Sentence().ClampLength(3, 500))
			.ToList();

		// Act
		var sut = new JobOpportunityRequirements(values);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Values.Should().NotBeNullOrEmpty();
		_ = sut.Values.Should().HaveCount(count);
		_ = sut.Values.Should().BeEquivalentTo(values);
	}

	[Fact(DisplayName = nameof(Constructor_WithValueTooShort_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValueTooShort_ShouldThrowException()
	{
		// Arrange
		var value = Faker.Lorem.Sentence().ClampLength(0, 2);
		var values = new List<string> { value };

		// Act
		var act = () => new JobOpportunityRequirements(values);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityRequirementTooShortException>()
			.WithMessage(JobOpportunityErrorMessages.RequirementTooShort(1));
	}

	[Fact(DisplayName = nameof(Constructor_WithValueTooLong_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValueTooLong_ShouldThrowException()
	{
		// Arrange
		var value = Faker.Lorem.Sentence().ClampLength(501, 1000);
		var values = new List<string> { value };

		// Act
		var act = () => new JobOpportunityRequirements(values);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityRequirementTooLongException>()
			.WithMessage(JobOpportunityErrorMessages.RequirementTooLong(1));
	}
}