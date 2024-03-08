// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.Exceptions.JobOpportunities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects;

/// <summary>
///   Unit tests for the <see cref="JobOpportunityContract" /> class.
/// </summary>
public sealed class JobOpportunityContractTests : BaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WhenGivenValidArguments_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Value, ValueObjectsTraits.Value)]
	public void Constructor_WhenGivenValidArguments_ShouldCreateAnInstance()
	{
		// Arrange
		var type = Faker.PickRandom<ContractType>();
		var minSalary = Faker.Random.Decimal(1000, 5000);
		var maxSalary = Faker.Random.Decimal(5000, 10000);

		// Act
		var sut = new JobOpportunityContract(type, minSalary, maxSalary);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Type.Should().Be(type);
		_ = sut.MinSalary.Should().Be(minSalary);
		_ = sut.MaxSalary.Should().Be(maxSalary);
	}

	[Fact(DisplayName = nameof(Constructor_WhenMinSalaryIsInvalid_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Value, ValueObjectsTraits.Value)]
	public void Constructor_WhenMinSalaryIsInvalid_ShouldThrowAnException()
	{
		// Arrange
		var type = Faker.PickRandom<ContractType>();
		var minSalary = Faker.Random.Decimal(-1000, 0);
		var maxSalary = Faker.Random.Decimal(5000, 10000);

		// Act
		var act = () => new JobOpportunityContract(type, minSalary, maxSalary);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityMinSalaryInvalidException>()
			.WithMessage(JobOpportunityErrorMessages.MinSalaryInvalid);
	}

	[Fact(DisplayName = nameof(Constructor_WhenMaxSalaryIsInvalid_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Value, ValueObjectsTraits.Value)]
	public void Constructor_WhenMaxSalaryIsInvalid_ShouldThrowAnException()
	{
		// Arrange
		var type = Faker.PickRandom<ContractType>();
		var minSalary = Faker.Random.Decimal(1000, 5000);
		var maxSalary = Faker.Random.Decimal(-1000, 0);

		// Act
		var act = () => new JobOpportunityContract(type, minSalary, maxSalary);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityMaxSalaryInvalidException>()
			.WithMessage(JobOpportunityErrorMessages.MaxSalaryInvalid);
	}

	[Fact(DisplayName = nameof(Constructor_WhenMinSalaryIsGreaterThanMaxSalary_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Value, ValueObjectsTraits.Value)]
	public void Constructor_WhenMinSalaryIsGreaterThanMaxSalary_ShouldThrowAnException()
	{
		// Arrange
		var type = Faker.PickRandom<ContractType>();
		var minSalary = Faker.Random.Decimal(1000, 5000);
		var maxSalary = Faker.Random.Decimal(0, 500);

		// Act
		var act = () => new JobOpportunityContract(type, minSalary, maxSalary);

		// Assert
		_ = act.Should()
			.ThrowExactly<JobOpportunityMinSalaryGreaterThanMaxSalaryException>()
			.WithMessage(JobOpportunityErrorMessages.MinSalaryGreaterThanMaxSalary);
	}
}