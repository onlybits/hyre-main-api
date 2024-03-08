// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions;
using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.JobOpportunities;

public sealed class JobOpportunityLocationTests : BaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WhenCreatingOnSiteOrHybrid_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WhenCreatingOnSiteOrHybrid_ShouldCreateAnInstance()
	{
		// Arrange
		var type = Faker.PickRandomWithout(LocationType.Remote);
		var city = Faker.Address.City();
		var state = Faker.Address.StateAbbr();

		// Act
		var sut = new JobOpportunityLocation(type, city, state);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Type.Should().Be(type);
		_ = sut.City.Should().NotBeEmpty();
		_ = sut.City.Should().Be(city);
		_ = sut.State.Should().NotBeEmpty();
		_ = sut.State.Should().Be(state);
	}

	[Fact(DisplayName = nameof(Constructor_WhenCreatingRemote_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WhenCreatingRemote_ShouldCreateAnInstance()
	{
		// Arrange
		const LocationType type = LocationType.Remote;
		var city = Faker.Address.City();
		var state = Faker.Address.StateAbbr();

		// Act
		var sut = new JobOpportunityLocation(type, city, state);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Type.Should().Be(type);
		_ = sut.City.Should().BeNullOrEmpty();
		_ = sut.State.Should().BeNullOrEmpty();
	}

	[Fact(DisplayName = nameof(Constructor_WithCityTooShort_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithCityTooShort_ShouldThrowADomainException()
	{
		// Arrange
		var city = Faker.Address.City().ClampLength(0, 2);
		var state = Faker.Address.StateAbbr();
		const LocationType type = LocationType.OnSite;

		// Act
		var act = () => new JobOpportunityLocation(type, city, state);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityLocationCityTooShortException>()
			.WithMessage(JobOpportunityErrorMessages.LocationCityNameTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithCityTooLong_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithCityTooLong_ShouldThrowADomainException()
	{
		// Arrange
		var city = Faker.Address.City().ClampLength(33, 100);
		var state = Faker.Address.StateAbbr();
		const LocationType type = LocationType.OnSite;

		// Act
		var act = () => new JobOpportunityLocation(type, city, state);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityLocationCityTooLongException>()
			.WithMessage(JobOpportunityErrorMessages.LocationCityNameTooLong);
	}

	[Fact(DisplayName = nameof(Constructor_WithStateTooShort_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithStateTooShort_ShouldThrowADomainException()
	{
		// Arrange
		var city = Faker.Address.City();
		var state = Faker.Address.StateAbbr().ClampLength(0, 1);
		const LocationType type = LocationType.OnSite;

		// Act
		var act = () => new JobOpportunityLocation(type, city, state);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityLocationStateTooShortException>()
			.WithMessage(JobOpportunityErrorMessages.LocationStateNameTooShort);
	}

	[Fact(DisplayName = nameof(Constructor_WithStateTooLong_ShouldThrowADomainException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithStateTooLong_ShouldThrowADomainException()
	{
		// Arrange
		var city = Faker.Address.City();
		var state = Faker.Address.StateAbbr().ClampLength(3, 100);
		const LocationType type = LocationType.OnSite;

		// Act
		var act = () => new JobOpportunityLocation(type, city, state);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityLocationStateTooLongException>()
			.WithMessage(JobOpportunityErrorMessages.LocationStateNameTooLong);
	}

	[Fact(DisplayName = nameof(Constructor_WhenCreatingOnSiteOrHybridWithNullValues_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WhenCreatingOnSiteOrHybridWithNullValues_ShouldThrowException()
	{
		// Arrange
		var type = Faker.PickRandomWithout(LocationType.Remote);
		const string? city = null;
		const string? state = null;

		// Act
		var act = () => new JobOpportunityLocation(type, city, state);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityLocationCantBeNullOnSiteOrHybridException>()
			.WithMessage(JobOpportunityErrorMessages.LocationCantBeNullOnSiteOrHybrid);
	}
}