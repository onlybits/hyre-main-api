﻿// Licensed to Hyre under one or more agreements.
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
///   Unit tests for the job opportunity extensions.
/// </summary>
public sealed class JobOpportunityExtensionsTests : JobOpportunityBaseFixture
{
	[Fact(DisplayName = nameof(ToResponse_WhenUsedInValidEntity_ShouldMapToResponseObject))]
	[Trait(ExtensionsTraits.Name, ExtensionsTraits.Value)]
	public void ToResponse_WhenUsedInValidEntity_ShouldMapToResponseObject()
	{
		// Arrange
		var jobOpportunity = GenerateJobOpportunity();

		// Act
		var response = jobOpportunity.ToResponse();

		// Assert
		_ = response.Should().NotBeNull();
		_ = response.Id.Should().Be(jobOpportunity.Id);
		_ = response.Name.Should().Be(jobOpportunity.Name);
		_ = response.Description.Should().Be(jobOpportunity.Description);
		_ = response.Location.Should().Be(jobOpportunity.Location);
		_ = response.Contract.Should().Be(jobOpportunity.Contract);
		_ = response.Requirements.Should().BeEquivalentTo(jobOpportunity.Requirements);
	}
}