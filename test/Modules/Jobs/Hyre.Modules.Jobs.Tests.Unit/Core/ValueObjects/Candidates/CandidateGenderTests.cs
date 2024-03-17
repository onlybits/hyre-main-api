// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Enums;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

/// <summary>
///   Unit tests for the <see cref="CandidateGender" /> class.
/// </summary>
public sealed class CandidateGenderTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidGender_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidGender_ShouldCreateAnInstance()
	{
		// Arrange
		var validGender = Faker.PickRandom<Gender>();

		// Act
		var sut = new CandidateGender(validGender);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().Be(validGender);
	}
}