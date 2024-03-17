// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

/// <summary>
///   Unit tests for the <see cref="CandidateDisability" /> class.
/// </summary>
public sealed class CandidateDisabilityTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		bool? hearing = Faker.Random.Bool();
		bool? vision = Faker.Random.Bool();
		bool? intellectual = Faker.Random.Bool();
		bool? physical = Faker.Random.Bool();

		// Act
		var sut = new CandidateDisability(hearing, vision, intellectual, physical);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Hearing.Should().Be(hearing);
		_ = sut.Vision.Should().Be(vision);
		_ = sut.Intellectual.Should().Be(intellectual);
		_ = sut.Physical.Should().Be(physical);
	}
}