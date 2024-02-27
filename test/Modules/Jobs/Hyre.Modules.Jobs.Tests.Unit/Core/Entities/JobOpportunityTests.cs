﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Modules.Jobs.Core.Entities;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.Entities;

/// <summary>
///   Contains tests for the <see cref="JobOpportunity" /> class.
/// </summary>
public sealed class JobOpportunityTests
{
	[Fact(DisplayName = nameof(Create_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(EntitiesTraits.Name, EntitiesTraits.Value)]
	public void Create_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange & Act
		var jobOpportunity = JobOpportunity.Create();

		// Assert
		_ = jobOpportunity.Should().NotBeNull();
		_ = jobOpportunity.Id.Should().NotBe(default!);
	}
}