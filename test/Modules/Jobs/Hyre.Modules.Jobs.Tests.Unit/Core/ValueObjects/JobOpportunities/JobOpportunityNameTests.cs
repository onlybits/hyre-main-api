﻿// Licensed to Hyre under one or more agreements.
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

public sealed class JobOpportunityNameTests : BaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var value = Faker.Name.JobTitle().ClampLength(3, 64);

		// Act
		var sut = new JobOpportunityName(value);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
	}

	[Fact(DisplayName = nameof(Constructor_WithValueTooShort_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValueTooShort_ShouldThrowADomainException()
	{
		// Arrange
		var value = Faker.Name.JobTitle().ClampLength(0, 2);

		// Act
		var act = () => new JobOpportunityName(value);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityNameTooShortException>()
			.WithMessage(JobOpportunityErrorMessages.NameTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithValueTooLong_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValueTooLong_ShouldThrowADomainException()
	{
		// Arrange
		var value = Faker.Name.JobTitle().ClampLength(65, 100);

		// Act
		var act = () => new JobOpportunityName(value);

		// Assert
		_ = act.Should().ThrowExactly<JobOpportunityNameTooLongException>()
			.WithMessage(JobOpportunityErrorMessages.NameTooLong);
	}
}