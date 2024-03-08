// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.Entities;

/// <summary>
///   Unit tests for the <see cref="Candidate" /> class.
/// </summary>
public sealed class CandidateTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Create_WhenGivenValidParameters_ShouldCreateAnInstance))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void Create_WhenGivenValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var name = GenerateValidName();

		// Act
		var sut = Candidate.Create(name);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Id.Should().NotBe(default!);
		_ = sut.Name.Should().Be(name);
		_ = sut.CreatedAt.Should().NotBe(default!);
		_ = sut.CreatedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
		_ = sut.Events.Should().HaveCount(1);
	}
}