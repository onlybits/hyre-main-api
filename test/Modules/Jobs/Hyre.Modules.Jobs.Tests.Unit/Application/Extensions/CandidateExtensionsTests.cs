// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Application.Extensions;
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
		var candidate = GenerateValidCandidate();

		// Act
		var response = candidate.ToResponse();

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().Be(candidate.Id);
		_ = response.Name.Should().Be(candidate.Name);
		_ = response.Email.Should().Be(candidate.Email);
		_ = response.JobOpportunityId.Should().Be(candidate.JobOpportunityId);
		_ = response.CreatedAt.Should().Be(candidate.CreatedAt);
	}
}