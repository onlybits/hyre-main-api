// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Delete;
using Hyre.Modules.Jobs.Core.Entities;
using Hyre.Modules.Jobs.Core.ValueObjects.JobOpportunities;
using Hyre.Modules.Jobs.Tests.Unit.Common;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Application.UseCases.JobOpportunities;

/// <summary>
///   Data provider for the <see cref="DeleteJobOpportunityUseCaseTests" /> class.
/// </summary>
public abstract class DeleteJobOpportunityUseCaseTestsFixture : JobOpportunityBaseFixture
{
	/// <summary>
	///   Generates a valid <see cref="JobOpportunity" />.
	/// </summary>
	/// <returns>It will return a valid <see cref="JobOpportunity" />.</returns>
	protected JobOpportunity GenerateValidJobOpportunity() => JobOpportunity.Create(
		GenerateValidName(),
		GenerateValidDescription());

	/// <summary>
	///   Generates a valid request for the <see cref="DeleteJobOpportunityUseCase" /> class.
	/// </summary>
	/// <returns>It will return a valid <see cref="DeleteJobOpportunityRequest" />.</returns>
	protected DeleteJobOpportunityRequest GenerateValidRequest() => new(JobOpportunityId.New(), false);
}