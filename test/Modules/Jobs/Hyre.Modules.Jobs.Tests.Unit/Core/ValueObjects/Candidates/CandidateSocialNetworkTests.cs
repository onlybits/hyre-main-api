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
///   Unit tests for the <see cref="CandidateSocialNetwork" /> class.
/// </summary>
public sealed class CandidateSocialNetworkTests
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		const string validLinkedIn = "linkedin";
		const string validGitHub = "github";

		// Act
		var sut = new CandidateSocialNetwork(validLinkedIn, validGitHub);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.LinkedIn.Should().Be(validLinkedIn);
		_ = sut.GitHub.Should().Be(validGitHub);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidLinkedIn_ShouldThrowAnException))]
	[InlineData("a")]
	[InlineData("linkedin with a very long name that exceeds the limit of characters allowed for LinkedIn")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidLinkedIn_ShouldThrowAnException(string invalidLinkedIn)
	{
		// Act
		var act = () => new CandidateSocialNetwork(invalidLinkedIn, null);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateSocialNetworkLinkedinException>()
			.WithMessage(CandidateErrorMessages.SocialNetworkLinkedinInvalid);
	}

	[Theory(DisplayName = nameof(Constructor_WithInvalidGitHub_ShouldThrowAnException))]
	[InlineData("a")]
	[InlineData("github with a very long name that exceeds the limit of characters allowed for GitHub")]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidGitHub_ShouldThrowAnException(string invalidGitHub)
	{
		// Act
		var act = () => new CandidateSocialNetwork(null, invalidGitHub);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateSocialNetworkGitHubException>()
			.WithMessage(CandidateErrorMessages.SocialNetworkGitHubInvalid);
	}
}