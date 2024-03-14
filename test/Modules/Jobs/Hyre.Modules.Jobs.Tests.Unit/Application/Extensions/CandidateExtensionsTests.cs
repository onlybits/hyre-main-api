// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Extensions;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.Extensions;

/// <summary>
///   Unit tests for the candidate extensions.
/// </summary>
public sealed class CandidateExtensionsTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(ToResponse_WhenUsedInValidEntity_ShouldMapToResponseObject))]
	[Trait(ExtensionsTraits.Name, ExtensionsTraits.Value)]
	public void ToResponse_WhenUsedInValidEntity_ShouldMapToResponseObject()
	{
		// Arrange
		var jobOpportunity = GenerateJobOpportunity();
		var candidate = GenerateCandidate(new List<JobOpportunity> { jobOpportunity });

		// Act
		var response = candidate.ToResponse();

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().Be(candidate.Id);
		_ = response.Name.Should().Be(candidate.Name);
		_ = response.Email.Should().Be(candidate.Email);
		_ = response.Document.Should().Be(candidate.Document);
		_ = response.DateOfBirth.Should().Be(candidate.DateOfBirth);
		_ = response.Seniority.Should().Be(candidate.Seniority);
		_ = response.Disability.Should().Be(candidate.Disability);
		_ = response.Gender.Should().Be(candidate.Gender);
		_ = response.PhoneNumber.Should().Be(candidate.PhoneNumber);
		_ = response.Address.Should().Be(candidate.Address);
		_ = response.Educations.Should().BeEquivalentTo(candidate.Educations);
		_ = response.Experiences.Should().BeEquivalentTo(candidate.Experiences);
		_ = response.SocialNetwork.Should().Be(candidate.SocialNetwork);
		_ = response.Languages.Should().BeEquivalentTo(candidate.Languages);
		_ = response.CreatedAt.Should().Be(candidate.CreatedAt);
	}
}